﻿using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderBL orderBL;
        public OrdersController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }
        [HttpPost("AddOrder")]
        public IActionResult AddOrders(OrderModel ordersModel)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "Id").Value);
                var orderData = this.orderBL.AddOrder(ordersModel, userId);
                if (orderData != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Placed Successfully", Response = orderData });
                }
                else 
                {
                   return this.BadRequest(new { Status = false, Message = "Enter Correct BookId" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }
    }
}