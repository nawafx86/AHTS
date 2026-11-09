namespace AHTS.Core.Interfaces.DLL;

public interface IAssetRepo
{
    public Task<DTOs.AseestWithNameDTO?> getAsset(int id);
    public Task<int> addAsset(Entities.Asset asset);

     public Task<int> changeStatus(DTOs.changeStatusDTO changeStatusDto);

     public Task<int> changeAssetEmployeeID(DTOs.changeAssetEmployeeID changeAssetEmployeeId);

     public Task<bool> isAssetExists(int AssetId);

     public Task<bool> changeStatusAndEmployeeId(DTOs.changeEmployeeIdAndStatus asset);

     public Task<int> getAllAssetCount();

     public Task<(int assignedCount, int availableCount)> getAllStatusCount();

     public Task<List<Entities.Asset>> getAllAssets(int pageNumber = 1, int pageSize = 10);

}   