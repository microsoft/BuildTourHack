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

using System.Linq;
using AutoMapper;
using Microsoft.Knowzy.Domain;
using Microsoft.Knowzy.Models.ViewModels;

namespace Microsoft.Knowzy.Models.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrdersViewModel>();

            CreateMap<Order, OrderViewModel>()
                .ForMember(orderViewModel => orderViewModel.PostalCarrierName,
                    options => options.ResolveUsing(order => order.PostalCarrier.Name))
                .ForMember(orderViewModel => orderViewModel.PostalCarrierId,
                    options => options.ResolveUsing(order => order.PostalCarrier.Id))
                .ForMember(orderViewModel => orderViewModel.OrderLines,
                    options => options.MapFrom(order => order.OrderLines.OrderBy(orderLine => orderLine.Id)));
        }
    }
}
