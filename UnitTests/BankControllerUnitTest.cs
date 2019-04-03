using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using HomeLoanInsuranceManagementApi.Services.Contracts;
using HomeLoanInsuranceManagementApi.Models;
using HomeLoanInsuranceManagementApi.Controllers;

namespace MoQTests
{

    public class BankControllerUnitTest
    {
        [Fact]
        public async void Test_Get_Bank()
        {
            var mock = new Mock<IBankService>();
            mock.Setup(p => p.Get(It.IsAny<string>())).Returns(Task.FromResult<Bank>(new Bank() { Id=Guid.NewGuid().ToString(), Name="RoyalBank" }));
            BanksController controller = new BanksController(mock.Object);
            var result = await controller.GetBank(Guid.NewGuid().ToString());
            var actionResult = Assert.IsType<ActionResult<Bank>>(result);
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var bank = Assert.IsType<Bank>(objectResult.Value);
            Assert.Equal("RoyalBank", bank.Name);
        }

        [Fact]
        public async void Test_Get_AllBanks()
        {
            var mock = new Mock<IBankService>();
            mock.Setup(p => p.GetAll()).Returns(Task.FromResult<IEnumerable<Bank>>(new List<Bank>() { new Bank() { Id = Guid.NewGuid().ToString(), Name = "RoyalBank" } }));
            BanksController controller = new BanksController(mock.Object);
            var result = await controller.GetAllBanks();
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Bank>>>(result);
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var bank = Assert.IsType<List<Bank>>(objectResult.Value);
            Assert.Equal("RoyalBank", bank[0].Name);
        }

        [Fact]
        public async void Test_Get_CreateBank()
        {
            var mock = new Mock<IBankService>();
            mock.Setup(p => p.Add(It.IsAny<Bank>())).Returns(Task.FromResult<Result>(new Result() { IsSuccess = true, Message = "Bank Record Created" }));
            BanksController controller = new BanksController(mock.Object);
            var result = await controller.Create(new Bank() { Id = Guid.NewGuid().ToString(), Name = "RoyalBank" });
            var actionResult = Assert.IsType<ActionResult<Result>>(result);
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnResult = Assert.IsType<Result>(objectResult.Value);
            Assert.True(returnResult.IsSuccess);
        }

        [Fact]
        public async void Test_Get_DeleteBank()
        {
            var mock = new Mock<IBankService>();
            mock.Setup(p => p.Remove(It.IsAny<String>())).Returns(Task.FromResult<Result>(new Result() { IsSuccess = true, Message = "Bank Record Deleted" }));
            BanksController controller = new BanksController(mock.Object);
            var result = await controller.Delete(Guid.NewGuid().ToString());
            var actionResult = Assert.IsType<ActionResult<Result>>(result);
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnResult = Assert.IsType<Result>(objectResult.Value);
            Assert.True(returnResult.IsSuccess);
        }


    }
}
