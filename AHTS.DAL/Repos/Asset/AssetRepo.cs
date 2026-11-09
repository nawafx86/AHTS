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

    async public Task<bool> changeStatusAndEmployeeId(changeEmployeeIdAndStatus asset)
    {
        if (!await isAssetExists(asset.AssetID)) return false;
        try
        {
            int result = await _context.Assets.Where(e => e.AssetId == asset.AssetID)
                .ExecuteUpdateAsync(s => s.SetProperty(u => u.Status, asset.Status)
                    .SetProperty(u => u.AssignedToEmployeeId, asset.EmployeeID));
            return result >= 1;

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

    async public Task<bool> isAssetExists(int AssetId)
    {
        try
        {
           bool AssetExists =  await _context.Assets.AnyAsync(e => e.AssetId == AssetId);
           return AssetExists;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    
    }

    async public Task<int> getAllAssetCount()
    {
        try
        {
            int count = await _context.Assets.CountAsync();
            return count;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    async public Task<(int assignedCount, int availableCount)> getAllStatusCount()
    {
        try
        {
            var countList = await _context.Assets.GroupBy(e => e.Status).Select(s =>
                new
                {
                    status = s.Key,
                    count = s.Count()
                }).ToListAsync();
            int assignedCount = countList.FirstOrDefault(s => s.status == "Assigned")?.count ?? 0;
            int availableCount = countList.FirstOrDefault(s => s.status == "Available")?.count ?? 0;
            return (assignedCount, availableCount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    async public Task<List<Core.Entities.Asset>> getAllAssets(int pageNumber = 1, int pageSize = 10)
    {
        try
        {

            var items = await _context.Assets
                .OrderBy(e => e.AssetId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return items;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}