using System.Data;
using AlifTech.Interview.Ewallet.Auth;
using AlifTech.Interview.Ewallet.Exceptions;
using AlifTech.Interview.Ewallet.Models;
using AlifTech.Interview.Ewallet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlifTech.Interview.Ewallet.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = DigestAuthenticationDefaults.AuthenticationScheme)]
public class EwalletController : ControllerBase
{
    /// <summary>
    ///   Check if wallet exists
    /// </summary>
    /// <param name="walletService"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("wallet-exists")]
    public async Task<IActionResult> WalletExistsAsync(
        [FromServices] IWalletService walletService,
        [FromBody] WalletExistsRequest request)
    {
        if (request == null)
            return BadRequest();

        var exists = await walletService.IsWalletExistsAsync(request.WalletId);
        if (exists)
            return Ok();

        return NotFound();
    }


    /// <summary>
    ///  Get wallet balance
    /// </summary>
    /// <param name="request"></param>
    /// <param name="walletService"></param>
    /// <returns></returns>
    [HttpPost("get-wallet-balance")]
    public async Task<IActionResult> GetWalletBalanceAsync(
        [FromBody] GetBalanceRequest request,
        [FromServices] IWalletService walletService)
    {
        if (request == null)
            return BadRequest();
        try
        {
            var balance = await walletService.GetBalanceAsync(request.WalletId);

            return Ok(balance);
        }
        catch (WalletNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    /// <summary>
    ///  Get replenishments for month
    /// </summary>
    /// <param name="request"></param>
    /// <param name="walletService"></param>
    /// <returns></returns>
    [HttpPost("get-replenishments-for-month")]
    public async Task<IActionResult> GetReplenishmentsForMonthAsync(
        [FromBody] GetReplenishmentsForMonthRequest request,
        [FromServices] IWalletService walletService)
    {
        if (request == null)
            return BadRequest();
        try
        {
            var response = await walletService
                .GetReplenishmentsForMonthAsync(request.WalletId, request.Month);

            return Ok(response);
        }
        catch (WalletNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    /// <summary>
    ///  Update wallet balance
    /// </summary>
    /// <param name="request"></param>
    /// <param name="walletService"></param>
    /// <returns></returns>
    [HttpPost("update-balance")]
    public async Task<IActionResult> UpdateBalanceAsync(
        [FromBody] UpdateBalanceRequest request,
        [FromServices] IWalletService walletService)
    {
        if (request == null)
            return BadRequest();

        try
        {
            var success = await walletService
                .UpdateBalanceAsync(request.WalletId, request.Amount);

            return Ok(success);
        }
        catch (WalletNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (WalletTypeNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (WalletBalanceException e)
        {
            return BadRequest(e.Message);
        }
        catch (DBConcurrencyException)
        {
            return Conflict("Failed to update balance");
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}