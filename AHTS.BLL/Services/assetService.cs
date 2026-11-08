using AHTS.Core.DTOs;
using AHTS.Core.Entities;
using AHTS.Core.Interfaces.BLL;
using AHTS.Core.Interfaces.DLL;

namespace AHTS.BLL.Services;

public class assetService : IAssetService
{
    private IAssetRepo _assetRepo;

    public assetService(IAssetRepo assetRepo)
    {
        _assetRepo = assetRepo;
    }
    
    public async Task<AseestWithNameDTO> getAsset(int id)
    {
        AseestWithNameDTO? Asset = await _assetRepo.getAsset(id);

        if (Asset == null)
        {
            return new AseestWithNameDTO
            {
                AssetId = -1
            };
        }

        return Asset;
    }

    public async Task<int> addAsset(AddNewAssetDTO assetDto)
    {
        Asset asset = new Asset
        {
            AssignedToEmployeeId = assetDto.AssignedToEmployeeId,
            Status = assetDto.Status,
            SerialNumber = assetDto.SerialNumber,
            DeviceType = assetDto.DeviceType
        };
       return await _assetRepo.addAsset(asset);
    }

    public async Task<int> changeAssetStatus(changeStatusDTO statusDto)
    {
        return await _assetRepo.changeStatus(statusDto);
    }

    public async Task<int> changeAssetEmployeeID(changeAssetEmployeeID employeeId)
    {
        return await _assetRepo.changeAssetEmployeeID(employeeId);
    }

    public async Task<bool> changeAssetEmployeeIDAndStatus(changeEmployeeIdAndStatus asset)
    {
        switch (asset.Status)
        {


            case "Assigned":
            {
                await changeAssetEmployeeID(new changeAssetEmployeeID
                    { EmployeeID = asset.EmployeeID, AssetID = asset.AssetID });
                await changeAssetStatus(new changeStatusDTO { AssetID = asset.AssetID, Status = asset.Status });
                return true;
            }
            case "Available":
            {
                await changeAssetEmployeeID(new changeAssetEmployeeID
                    { EmployeeID = null, AssetID = asset.AssetID });
                await changeAssetStatus(new changeStatusDTO { AssetID = asset.AssetID, Status = asset.Status });
                return true;
            }
                default: return false;

        }


    }
}