using AHTS.Core.DTOs;
using AHTS.Core.Interfaces.DLL;
using AHTS.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AHTS.DAL.Repos.Asset;

public class AssetRepo : IAssetRepo
{
    private readonly AhtsContext _context;
    public AssetRepo(AhtsContext context)
    {
        _context = context;
    }
    async public Task<AseestWithNameDTO?> getAsset(int id)
    {
        AseestWithNameDTO? asset = await _context.Assets.Where(e => e.AssetId == id)
            .Select(e => new AseestWithNameDTO
            {
                AssetId = e.AssetId,
                SerialNumber = e.SerialNumber,
                Status = e.Status,
                Name = e.AssignedToEmployee != null ? e.AssignedToEmployee.Name : string.Empty,
                AssignedToEmployeeId = e.AssignedToEmployeeId,
                DeviceType = e.DeviceType

            }).FirstOrDefaultAsync();
        if (asset != null)
        {
            return asset;
        }
        return null;
    }

    async public Task<int> addAsset(Core.Entities.Asset asset)
    {
        try
        {
            _context.Assets.Add(asset);
           await _context.SaveChangesAsync();
           return asset.AssetId;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    async public Task<int> changeStatus(changeStatusDTO changeStatusDto)
    {
        try
        {
            return await _context.Assets.Where(e => e.AssetId == changeStatusDto.AssetID)
                .ExecuteUpdateAsync(s => s.SetProperty(u => u.Status, changeStatusDto.Status));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
    }

    async public Task<int> changeAssetEmployeeID(changeAssetEmployeeID changeAssetEmployeeId)
    {
        try
        {
            return await _context.Assets.Where(e => e.AssetId == changeAssetEmployeeId.AssetID)
                .ExecuteUpdateAsync(s => s.SetProperty(u => u.AssignedToEmployeeId, changeAssetEmployeeId.EmployeeID));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}