namespace AHTS.Core.Interfaces.BLL;

public interface IAssetService
{
    public Task<DTOs.AseestWithNameDTO> getAsset(int id);
    
    public Task<int> addAsset(DTOs.AddNewAssetDTO newAssetDto);

    public Task<int> changeAssetStatus(DTOs.changeStatusDTO changeStatusDto);

    public Task<int> changeAssetEmployeeID(DTOs.changeAssetEmployeeID changeAssetEmployeeId);
    public Task<bool> changeAssetEmployeeIDAndStatus(DTOs.changeEmployeeIdAndStatus changeEmployeeIdAndStatus);
    public Task<bool> isAssetExists(int AssetId);
    
    public Task<int> getAllAssetCount();

    public Task<(int assignedCount, int availableCount)> getAllStatusCount();

    public Task<List<DTOs.AssetDTO>> getAllAssets(int pageNumber, int pageSize);

}