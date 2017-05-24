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

using System.ComponentModel.DataAnnotations;
using Microsoft.Knowzy.Domain;

namespace Microsoft.Knowzy.Models.ViewModels
{
    public class OrdersViewModel
    {
        [Display(Name = "Order No.")]
        public string Id { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        public string Tracking { get; set; }
        public OrderStatus Status { get; set; }
    }
}
