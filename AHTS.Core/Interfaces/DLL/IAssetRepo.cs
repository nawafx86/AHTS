namespace AHTS.Core.Interfaces.DLL;

public interface IAssetRepo
{
    public Task<DTOs.AseestWithNameDTO?> getAsset(int id);
    public Task<int> addAsset(Entities.Asset asset);

     public Task<int> changeStatus(DTOs.changeStatusDTO changeStatusDto);

     public Task<int> changeAssetEmployeeID(DTOs.changeAssetEmployeeID changeAssetEmployeeId);

     public Task<bool> isAssetExists(int AssetId);

     public Task<bool> changeStatusAndEmployeeId(DTOs.changeEmployeeIdAndStatus asset);


}