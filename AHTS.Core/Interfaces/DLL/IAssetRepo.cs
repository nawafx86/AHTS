namespace AHTS.Core.Interfaces.DLL;

public interface IAssetRepo
{
    public Task<Core.DTOs.AseestWithNameDTO?> getAsset(int id);
    public Task<int> addAsset(Core.Entities.Asset asset);

     public Task<int> changeStatus(Core.DTOs.changeStatusDTO changeStatusDto);

     public Task<int> changeAssetEmployeeID(Core.DTOs.changeAssetEmployeeID changeAssetEmployeeId);


}