// ******************************************************************
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Knowzy.Domain;
using Microsoft.Knowzy.Models;
using Microsoft.Knowzy.Models.ViewModels;
using Micrososft.Knowzy.Repositories.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Microsoft.Knowzy.Repositories.Core
{
    public class OrderRepositoryMock : IOrderRepository
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        private List<Shipping> _shippings = new List<Shipping>();
        private List<Receiving> _receivings = new List<Receiving>();
        private List<Product> _products = new List<Product>();
        private List<Customer> _customers = new List<Customer>();
        private List<PostalCarrier> _postalCarriers = new List<PostalCarrier>();

        #endregion

        #region Constructor

        public OrderRepositoryMock(IMapper mapper, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;

            Seed().Wait();
        }

        #endregion

        #region Public Methods
        public async Task<IEnumerable<ShippingsViewModel>> GetShippings()
        {
            return await Task.Run(() => _mapper.Map<IEnumerable<ShippingsViewModel>>(_shippings));
        }

        public async Task<IEnumerable<ReceivingsViewModel>> GetReceivings()
        {
            return await Task.Run(() => _mapper.Map<IEnumerable<ReceivingsViewModel>>(_receivings));
        }

        public async Task<ShippingViewModel> GetShipping(string orderId)
        {
            return await Task.Run(() => _mapper.Map<ShippingViewModel>(_shippings.FirstOrDefault(shipping => shipping.Id == orderId)));
        }

        public async Task<ReceivingViewModel> GetReceiving(string orderId)
        {
            return await Task.Run(() => _mapper.Map<ReceivingViewModel>(_receivings.FirstOrDefault(receiving => receiving.Id == orderId)));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await Task.Run(() => _products);
        }

        public async Task<IEnumerable<PostalCarrier>> GetPostalCarriers()
        {
            return await Task.Run(() => _postalCarriers);
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await Task.Run(() => _customers);
        }

        public async Task<int> GetShippingCount()
        {
            return await Task.Run(() =>_shippings.Count);
        }

        public async Task<int> GetReceivingCount()
        {
            return await Task.Run(() => _receivings.Count);
        }

        public async Task<int> GetProductCount()
        {
            return await Task.Run(() => _products.Count);
        }

        public async Task AddShipping(Shipping shipping)
        {
            await Task.Run(() =>
            {
                shipping.Id = OrderRepositoryHelper.GenerateString(10);
                UpdateOrder(shipping);
                _shippings.Add(shipping);
            });
        }

        public async Task UpdateShipping(Shipping shipping)
        {
            await Task.Run(() =>
            {
                var index = _shippings.FindIndex(existingShipping => existingShipping.Id == shipping.Id);
                if (index != -1)
                {
                    UpdateOrder(shipping);
                    _shippings[index] = shipping;
                }
            });
        }       

        public async Task AddReceiving(Receiving receiving)
        {
            await Task.Run(() =>
            {
                receiving.Id = OrderRepositoryHelper.GenerateString(10);
                UpdateOrder(receiving);
                _receivings.Add(receiving);
            });
        }

        public async Task UpdateReceiving(Receiving receiving)
        {
            await Task.Run(() =>
            {
                var index = _receivings.FindIndex(existingReceiving => existingReceiving.Id == receiving.Id);
                if (index != -1)
                {
                    UpdateOrder(receiving);
                    _receivings[index] = receiving;
                }
            });
        }

        #endregion

        #region Private Methods

        private void UpdateOrder(Order order)
        {
            foreach (var orderLine in order.OrderLines)
            {
                var productInOrderLine = _products.FirstOrDefault(product => product.Id == orderLine.ProductId);
                orderLine.Product = productInOrderLine;
            }

            order.PostalCarrier =
                _postalCarriers.FirstOrDefault(postalCarrier => postalCarrier.Id == order.PostalCarrierId);
        }

        private async Task Seed()
        {
            var customerJsonPath = Path.Combine(_hostingEnvironment.WebRootPath, _configuration["AppSettings:CustomerJsonPath"]);
            var productJsonPath = Path.Combine(_hostingEnvironment.WebRootPath, _configuration["AppSettings:ProductJsonPath"]);
            var orderJsonPath = Path.Combine(_hostingEnvironment.WebRootPath, _configuration["AppSettings:OrderJsonPath"]);

            await SeedCustomers(customerJsonPath);
            await SeedProducts(productJsonPath);
            await SeedOrders(orderJsonPath);
        }

        private async Task SeedOrders(string orderJsonPath)
        {
            var data = await GetData<OrderImport>(orderJsonPath);

            if (data?.Receivings != null)
            {
                _receivings = data.Receivings;

                foreach (var receiving in _receivings)
                {
                    foreach (var orderLine in receiving.OrderLines)
                    {
                        var productInOrderLine = _products.FirstOrDefault(product => product.Id == orderLine.ProductId);
                        orderLine.Product = productInOrderLine;
                    }
                }
            }

            if (data?.Shippings != null)
            {
                _shippings = data.Shippings;

                foreach (var shipping in _shippings)
                {
                    foreach (var orderLine in shipping.OrderLines)
                    {
                        var productInOrderLine = _products.FirstOrDefault(product => product.Id == orderLine.ProductId);
                        orderLine.Product = productInOrderLine;
                    }
                }
            }

            _postalCarriers = _shippings.Select(shipping => shipping.PostalCarrier)
                              .GroupBy(postalCarrier => postalCarrier.Id)
                              .Select(postalCarrier => postalCarrier.First())
                              .ToList();
        }

        private async Task SeedProducts(string productJsonPath)
        {
            var data = await GetData<List<Product>>(productJsonPath);

            if (data != null)
            {
                _products = data;
            }
        }

        private async Task SeedCustomers(string customerJsonPath)
        {
            var data = await GetData<List<Customer>>(customerJsonPath);

            if (data != null)
            {
                _customers = data;
            }
        }

        private static async Task<T> GetData<T>(string jsonPath)
        {
            var dataAsString = await ReadDataFromFile(jsonPath);
            return JsonConvert.DeserializeObject<T>(dataAsString, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        private static async Task<string> ReadDataFromFile(string path)
        {
            string result;

            using (var reader = File.OpenText(path))
            {
                result = await reader.ReadToEndAsync();
            }

            return result;
        }

        #endregion
    }
}
