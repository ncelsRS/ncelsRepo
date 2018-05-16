using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teme.Common.Data.InDto;
using Teme.Common.Data.OutDto;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Primitives;
using Teme.Shared.Data.Repos.IconRepo;


namespace Teme.Common.Data.Icons
{
    public class IconRepo : BaseIconRepo, IIconRepo
    {
        public IconRepo(TemeContext context) : base(context)
        {
        }
        public async Task<Icon> GetIcon(IconGetInDto cm)
        {
            return await Context.Icons.Where(e => !e.isDeleted && e.ModuleType == cm.ModuleType && e.ObjectId == cm.ObjectId
                       && e.FieldName == cm.FieldName)
                        .FirstOrDefaultAsync();
        }
        public async Task<IconOutDto> GetIconOutDto(IconGetInDto cm)
        {
            return await Context.Icons.Where(e => !e.isDeleted && e.ModuleType == cm.ModuleType && e.ObjectId == cm.ObjectId
                       && e.FieldName == cm.FieldName)
                        .Select(x => new IconOutDto{
                            Id = x.Id,
                            ModuleType = x.ModuleType,
                            ObjectId = x.ObjectId,
                            FieldName = x.FieldName,
                            IconRecords = x.IconRecords.Select(ir => new IconRecordOutDto {
                                id = ir.Id,
                                UserName = ir.AuthUser.FirstName,
                                RoleName = ir.AuthRoleId.ToString(),
                                ValueField = ir.ValueField,
                                DisplayField = ir.DisplayField,
                                Note = ir.Note,
                                DateCreate = ir.DateCreate,
                            })
                        })
                        .FirstOrDefaultAsync();
        }
        public async Task CreateIcon(Icon icon)
        {
            Context.Icons.Add(icon);
            await Context.SaveChangesAsync();
        }
        public async Task CreateIconRecord(IconRecord iconRecord)
        {
            Context.IconRecords.Add(iconRecord);
            await Context.SaveChangesAsync();
        }

    }
}

