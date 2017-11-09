using System;
using System.Collections.Generic;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Ncels.Database.Models
{
	public class UnitModel
	{
		public UnitModel()
		{
			Id = Guid.NewGuid();
		}

		public UnitModel(Unit unit)
		{
			Id = unit.Id;
			Code = unit.Code;
			Name = unit.Name;
			NameKz = unit.NameKz;
			ShortName = unit.ShortName;
			UnitTypeDictionaryValue = unit.UnitTypeDictionaryValue;
			Type = unit.Type;
			Rank = unit.Rank;
			NameFull = LocalizationHelper.GetString(unit.Name, unit.NameKz);
			PositionState = unit.PositionState;
			Email = unit.Email;
		    Bin = unit.Bin;
			ParentId = unit.ParentId;
			UnitTypeDictionaryId = DictionaryHelper.GetItems(unit.UnitTypeDictionaryId, unit.UnitTypeDictionaryValue);
			BossId = DictionaryHelper.GetItems(unit.BossId, unit.BossValue);
			ManagerId = DictionaryHelper.GetItems(unit.ManagerId, unit.ManagerValue);
			SecretaryId = DictionaryHelper.GetItems(unit.SecretaryId, unit.SecretaryValue);
			ChancelleryId = DictionaryHelper.GetItems(unit.ChancelleryId, unit.ChancelleryValue);
			CuratorId = DictionaryHelper.GetItems(unit.CuratorId, unit.CuratorValue);
			Category = unit.UnitTypeDictionaryId;
			PositionType = unit.PositionType;
			PositionStaff = unit.PositionStaff;
		}

		public Unit GetUnit(Unit unit)
		{
			unit.Id = Id;
			unit.Code = Code;
			unit.Name = Name;
			unit.NameKz = NameKz;
			unit.ShortName = ShortName;
			unit.UnitTypeDictionaryValue = UnitTypeDictionaryValue;
			unit.Type = Type;
			unit.PositionState = PositionState;
			unit.Email = Email;
			unit.Rank = Rank;
		    unit.Bin = Bin;

			unit.ParentId = ParentId;
			unit.UnitTypeDictionaryId = DictionaryHelper.GetItemsId(UnitTypeDictionaryId);
			unit.BossId = DictionaryHelper.GetItemsId(BossId);
			unit.ManagerId = DictionaryHelper.GetItemsId(ManagerId);
			unit.SecretaryId = DictionaryHelper.GetItemsId(SecretaryId);
			unit.ChancelleryId = DictionaryHelper.GetItemsId(ChancelleryId);
			unit.CuratorId = DictionaryHelper.GetItemsId(CuratorId);

			unit.UnitTypeDictionaryValue = DictionaryHelper.GetItemsName(UnitTypeDictionaryId);
			unit.BossValue = DictionaryHelper.GetItemsName(BossId);
			unit.ManagerValue = DictionaryHelper.GetItemsName(ManagerId);
			unit.SecretaryValue = DictionaryHelper.GetItemsName(SecretaryId);
			unit.ChancelleryValue = DictionaryHelper.GetItemsName(ChancelleryId);
			unit.CuratorValue = DictionaryHelper.GetItemsName(CuratorId);
			unit.PositionType = PositionType;
			unit.PositionStaff = PositionStaff;
			return unit;
		}
		public string Category { get; set; }
		public Guid Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string NameKz { get; set; }

		public string NameFull { get; set; }
		public string ShortName { get; set; }
		public List<Item> UnitTypeDictionaryId { get; set; }
		public string UnitTypeDictionaryValue { get; set; }
		public List<Item> BossId { get; set; }
		public int Rank { get; set; }
        public string Bin { get; set; }


	    public int PositionState { get; set; }
		public int Type { get; set; }
		public string Email { get; set; }

		public Guid? ParentId { get; set; }
		public List<Item> ManagerId { get; set; }
		public List<Item> SecretaryId { get; set; }
		public List<Item> ChancelleryId { get; set; }
		public List<Item> CuratorId { get; set; }
		public int PositionType { get; set; }

		public int PositionStaff { get; set; }

	}
}