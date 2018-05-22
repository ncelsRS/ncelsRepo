using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teme.Common.Data.Icons;
using Teme.Common.Data.InDto;
using Teme.Common.Data.OutDto;
using Teme.Shared.Data.Context;
using Teme.Shared.Logic.IconLogic;

namespace Teme.Common.Logic.Icons
{
    public class IconLogic : BaseIconLogic<IIconRepo>, IIconLogic
    {
        private readonly IIconRepo _repo;
        private readonly IConvertOutDtoRepo _outDtoRepo;

        public IconLogic(IIconRepo repo, IConvertOutDtoRepo outDtoRepo) : base(repo)
        {
            _repo = repo;
            _outDtoRepo = outDtoRepo;
        }

        /// <summary>
        /// Создание Айк
        /// </summary>
        /// <returns></returns>
        public async Task<object> CreateIconRecord(IconCreateInDto cm)
        {
            Icon icon = await _repo.GetIcon(cm);
            if (icon == null)
            {
                icon = new Icon() { ModuleType = cm.ModuleType, ObjectId = cm.ObjectId, FieldName = cm.FieldName, isError=true };
                await _repo.CreateIcon(icon);
            } else if (!icon.isError)
            {
                icon.isError = true;
                await _repo.UpdateIcon(icon);
            }
            var iconRecord = new IconRecord()
            {
                IconId = icon.Id,
                AuthUserId = 1,
                AuthRoleId = 1,
                ValueField = cm.ValueField,
                DisplayField = cm.DisplayField,
                Note = cm.Note
            };
            await _repo.CreateIconRecord(iconRecord);
            return new { iconRecord.Id };
        }

        /// <summary>
        /// Получить Айку
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetIconRecords(IconGetInDto gid)
        {
            var IconOutDto = await _repo.GetIconOutDto(gid);
            //IconOutDto iconOutDto = _outDtoRepo.ConvertEntityToIcon(icon);
            return IconOutDto;
        }

        /// <summary>
        /// Изменить Статус Айки
        /// </summary>
        /// <returns></returns>
        public async Task<object> UpdateIconError(IconCreateInDto cm)
        {
            Icon icon = await _repo.GetIcon(cm);
            if (icon != null)
            {
                if (icon.isError)
                {
                    icon.isError = false;
                    await _repo.UpdateIcon(icon);
                }
                
            }
            return new { icon.Id };
        }



    }
}
