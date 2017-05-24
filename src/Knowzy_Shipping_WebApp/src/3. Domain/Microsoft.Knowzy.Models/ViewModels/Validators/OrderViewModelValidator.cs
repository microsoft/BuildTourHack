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

using FluentValidation;

namespace Microsoft.Knowzy.Models.ViewModels.Validators
{
    public class OrderViewModelValidator : AbstractValidator<OrderViewModel>
    {
        public OrderViewModelValidator()
        {            
            RuleFor(order => order.Address).NotEmpty().WithMessage("Address cannot be empty");
            RuleFor(order => order.CompanyName).NotEmpty().WithMessage("Company Name cannot be empty");
            RuleFor(order => order.Email).EmailAddress().WithMessage("Introduce a valid email").NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(order => order.ContactPerson).NotEmpty().WithMessage("Contact Person cannot be empty");
            RuleFor(order => order.PhoneNumber).NotEmpty().WithMessage("Phone Number cannot be empty");
            RuleFor(order => order.PostalCarrierId).NotEmpty().WithMessage("Postal Carrier cannot be empty");
        }
    }
}
