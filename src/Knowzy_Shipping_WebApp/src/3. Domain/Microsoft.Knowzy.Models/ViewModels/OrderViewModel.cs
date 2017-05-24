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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Knowzy.Domain;
using Microsoft.Knowzy.Models.ViewModels.Validators;

namespace Microsoft.Knowzy.Models.ViewModels
{
    public class OrderViewModel : IValidatableObject
    {
        [Display(Name = "Order Number:")]
        public string Id { get; set; }
        [Display(Name = "Company name:")]
        public string CompanyName { get; set; }
        [Display(Name = "Address:")]
        public string Address { get; set; }
        [Display(Name = "Contact Person:")]
        public string ContactPerson { get; set; }
        [Display(Name = "Contact email:")]
        public string Email { get; set; }
        [Display(Name = "Phone number:")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Tracking:")]
        public string Tracking { get; set; }
        [Display(Name = "Postal Carrier:")]
        public string PostalCarrierName { get; set; }
        public int PostalCarrierId { get; set; }
        [Display(Name = "Status:")]
        public OrderStatus Status { get; set; }
        public string CustomerId { get; set; }

        public List<OrderLineViewModel> OrderLines { get; set; }


        public int MaxAvailableItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new OrderViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
