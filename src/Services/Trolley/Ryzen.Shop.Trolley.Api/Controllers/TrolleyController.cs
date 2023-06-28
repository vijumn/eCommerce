﻿using FluentValidation;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ryzen.Shop.Trolley.Api.Model;
using Ryzen.Shop.Trolley.Api.Services;
using Ryzen.Shop.Trolley.Api.ViewModel;
using Ryzen.Shop.Trolley.Api.Validators;

namespace Ryzen.Shop.Trolley.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TrolleyController : ControllerBase
    {
        private readonly ILogger<TrolleyController> _logger;
        private readonly ITrolleyService _trolleyService;
        private IValidator<TrolleyViewModel> _validator;

        public TrolleyController(ILogger<TrolleyController> logger, ITrolleyService trolleyService, IValidator<TrolleyViewModel> validator)
        {
            _logger = logger;
            _trolleyService = trolleyService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string customerId)
        {
            var trolley = await _trolleyService.GetTrolley(customerId); 
            if(trolley == null)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(string customerId, TrolleyViewModel customerTrolley)
        {
            var result =  await _validator.ValidateAsync(customerTrolley);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var trolley = await _trolleyService.UpdateTrolley(customerId, MapToTrolley(customerId,customerTrolley));
            return Ok(trolley);
        }


        #region Private Methods
        private CustomerTrolley MapToTrolley(string customerId, TrolleyViewModel customerTrolley)
        {
            if (customerTrolley == null)
            {
                return null;
            }

            var trolley = new CustomerTrolley
            {
                CustomerId = customerId
            };

            customerTrolley.Items.ForEach(item => trolley.Items.Add(new TrolleyItem
            {

                ProductId = item.ProductId,
                Quantity = item.Quantity,
            })); 

            return trolley;
        }
        #endregion
    }
}
