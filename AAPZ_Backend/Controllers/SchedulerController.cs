﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AAPZ_Backend.Repositories;
using AAPZ_Backend.Models;
using AAPZ_Backend.BusinessLogic.Classes;

namespace AAPZ_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        ClientRepository clientDB;
        WorkplaceOrderRepository workplaceOrderDB;
        BuildingRepository buildingDB;
        WorkplaceRepository workplaceDB;

        public SchedulerController(ClientRepository repository)
        {
            clientDB = repository;
            workplaceOrderDB = new WorkplaceOrderRepository();
            workplaceDB = new WorkplaceRepository();
            buildingDB = new BuildingRepository();
        }


        [ProducesResponseType(typeof(List<Scheduler>), StatusCodes.Status200OK)]
        [HttpGet("GetSchedule/{clientId}")]
        public IActionResult GetSchedule(int clientId)
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Client client = clientDB.GetCurrentClient(userJWTId);
            if (client == null)
                return NotFound();

            List<WorkplaceOrder> workplaceOrders = workplaceOrderDB.GetEntityListByClientId(client.Id).ToList();

            List<Scheduler> schedulers = new List<Scheduler>();

            foreach (WorkplaceOrder item in workplaceOrders)
            {
                Workplace workplace = workplaceDB.GetEntity(item.WorkplaceId);
                Building building = buildingDB.GetEntity(workplace.BuildingId);
                schedulers.Add(new Scheduler(item.Id.ToString(), item.StartTime.ToString("yyyy-MM-dd HH:mm:ss"), 
                    item.FinishTime.ToString("yyyy-MM-dd HH:mm:ss"), "Назв:"+building.Name+"\nАдр:" + building.Country.ToString() + "," 
                    + building.City.ToString() + "," + building.Street.ToString() + "," + building.House.ToString() 
                    + "," + building.Flat.ToString() + "\nМісце:" + workplace.Id.ToString()  + "\nСума:" 
                    + item.SumToPay.ToString(), item.SumToPay.ToString()));
            }
            return new ObjectResult(schedulers);

        }
    }
}