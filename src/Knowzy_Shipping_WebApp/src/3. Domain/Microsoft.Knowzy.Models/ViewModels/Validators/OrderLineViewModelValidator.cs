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
    public class OrderLineViewModelValidator : AbstractValidator<OrderLineViewModel>
    {
        public OrderLineViewModelValidator()
        {
            RuleFor(orderLine => orderLine.Quantity)
                .NotEmpty()
                .WithMessage("A number between 1 - 999 is required")
                .GreaterThan(0)
                .WithMessage("At least one item is mandatory")
                .LessThanOrEqualTo(999)
                .WithMessage("Not more than 999 can be purchased")
                .Must(quantity => quantity is int)
                .WithMessage("A number between 1 - 999 is required");
        }
    }
}
