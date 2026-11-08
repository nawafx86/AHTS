using AHTS.Core.Interfaces.BLL;
using Microsoft.AspNetCore.Mvc;

namespace AHTS.WEB.controllers.Asset;

[ApiController]
[Route("/AssetController")]
public class AssetController : ControllerBase
{
    private IAssetService _assetService;

    public AssetController(IAssetService assetService)
    {
        _assetService = assetService;
    }
    
    [HttpGet("/{id}/getAsset", Name = "getAssetByID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    async public Task<ActionResult<Core.DTOs.AseestWithNameDTO>> getAsset(int id)
    {
        Core.DTOs.AseestWithNameDTO assetDTOs = await _assetService.getAsset(id);
        return Ok(assetDTOs);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    async public Task<ActionResult> addAsset([FromQuery] Core.DTOs.AddNewAssetDTO assetDto)
    {
         int AssetID = await _assetService.addAsset(assetDto);
         return CreatedAtRoute(
         
             routeName: "getAssetByID",          
             routeValues: new { id = AssetID },
             value: new
             {
                 message = $"New Asset Created With the ID: {AssetID}",
                 assetDto
             }
         );
    }

    [HttpPatch("changeStatus")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    async public Task<ActionResult<int>> changeStatus([FromQuery] Core.DTOs.changeStatusDTO changeStatusDto)
    {
        return Ok(await _assetService.changeAssetStatus(changeStatusDto));
    }
    
    [HttpPatch("changeEmployeeId")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    async public Task<ActionResult<int>> changeEmployeeId([FromQuery] Core.DTOs.changeAssetEmployeeID changeAssetEmployeeIdAndStatus)
    {
        return Ok(await _assetService.changeAssetEmployeeID(changeAssetEmployeeIdAndStatus));
    }

    [HttpPatch("changeEmployeeIdAndStatus")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    async public Task<ActionResult<int>> changeEmployeeIdAndStatus([FromQuery] Core.DTOs.changeEmployeeIdAndStatus changeAssetEmployeeIdAndStatus)
    {
        return Ok(await _assetService.changeAssetEmployeeIDAndStatus(changeAssetEmployeeIdAndStatus));
    }
}