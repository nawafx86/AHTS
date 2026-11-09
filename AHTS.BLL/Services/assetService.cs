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
               return await _assetRepo.changeStatusAndEmployeeId(asset);
            }
            case "Available":
            {
                asset.EmployeeID = null;
                return await _assetRepo.changeStatusAndEmployeeId(asset);
            }
                default: return false;

        }


    }

    public async Task<bool> isAssetExists(int AssetId)
    {
        return await _assetRepo.isAssetExists(AssetId);
    }

   async public Task<List<AssetDTO>> getAllAssets(int pageNumber, int pageSize)
   {
       var assets =  await _assetRepo.getAllAssets(pageNumber, pageSize);
       var assetsDTO = new List<AssetDTO>();
            assets.ForEach(e => assetsDTO.Add(
           new AssetDTO
           {
               AssetId = e.AssetId,
               AssignedToEmployeeId = e.AssignedToEmployeeId,
               Status = e.Status,
               SerialNumber = e.SerialNumber,
               DeviceType = e.DeviceType,

           }));
            return assetsDTO;
   }

   async public Task<(int assignedCount, int availableCount)> getAllStatusCount()
   {
       return await _assetRepo.getAllStatusCount();
   }

   async public Task<int> getAllAssetCount()
   {
       return await _assetRepo.getAllAssetCount();
   }

}