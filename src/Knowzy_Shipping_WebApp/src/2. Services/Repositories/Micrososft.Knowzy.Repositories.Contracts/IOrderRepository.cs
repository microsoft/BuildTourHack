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
using System.Threading.Tasks;
using Microsoft.Knowzy.Domain;
using Microsoft.Knowzy.Models.ViewModels;

namespace Micrososft.Knowzy.Repositories.Contracts
{
    public interface IOrderRepository
    {
        Task<IEnumerable<ShippingsViewModel>> GetShippings();
        Task<IEnumerable<ReceivingsViewModel>> GetReceivings();
        Task<ShippingViewModel> GetShipping(string orderId);
        Task<ReceivingViewModel> GetReceiving(string orderId);
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<PostalCarrier>> GetPostalCarriers();
        Task<IEnumerable<Customer>> GetCustomers();
        Task<int> GetShippingCount();
        Task<int> GetReceivingCount();
        Task<int> GetProductCount();
        Task AddShipping(Shipping shipping);
        Task UpdateShipping(Shipping shipping);
        Task AddReceiving(Receiving receiving);
        Task UpdateReceiving(Receiving receiving);
    }
}
