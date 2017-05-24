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

using AutoMapper;
using Microsoft.Knowzy.Domain;
using Microsoft.Knowzy.Models.ViewModels;

namespace Microsoft.Knowzy.Models.Profiles
{
    public class OrderLineProfile : Profile
    {
        public OrderLineProfile()
        {
            CreateMap<OrderLine, OrderLineViewModel>()
                .ForMember(orderLineViewModel => orderLineViewModel.ProductImage,
                    options => options.ResolveUsing(orderLine => orderLine.Product.Image))
                .ForMember(orderLineViewModel => orderLineViewModel.ProductPrice,
                    options => options.ResolveUsing(orderLine => orderLine.Product.Price)); ;

            CreateMap<OrderLineViewModel, OrderLine>()
                .ForMember(orderLine => orderLine.Price,
                    options => options.ResolveUsing(
                        orderLineViewModel => orderLineViewModel.Quantity * orderLineViewModel.ProductPrice));
        }
    }
}
