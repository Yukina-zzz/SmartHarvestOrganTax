﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace SmartHarvestOrganTax
{
    public class SmartHarvestOrganTaxSettings : ModSettings
    {
        public bool IsLeft = true;
        public bool IsChangeMedCare = true;
        public bool IsLiverFirst = true;
        public MedicalCareCategory surgeryMedCare = MedicalCareCategory.HerbalOrWorse;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref IsLeft, "isLeft", true);
            Scribe_Values.Look(ref surgeryMedCare, "surgeryMedCare", MedicalCareCategory.HerbalOrWorse);
        }
    }
    public class AutoHarvestMod : Mod
    {
        public static SmartHarvestOrganTaxSettings settings;
        public AutoHarvestMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<SmartHarvestOrganTaxSettings>();
        }
        public override string SettingsCategory() => "Smart Harvest Organ Tax";
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard list = new Listing_Standard();
            list.Begin(inRect);

            /* ------------ 勾选框：左优先 ------------- */
            list.CheckboxLabeled(
                "SmartHarvestOrganTax_Setting_LeftFirst_Label".Translate(),
                ref settings.IsLeft,
                "SmartHarvestOrganTax_Setting_LeftFirst_Desc".Translate());
            list.GapLine();

            /* ------------ 勾选框：左优先 ------------- */
            list.CheckboxLabeled(
                "SmartHarvestOrganTax_Setting_IsLiverFirst_Label".Translate(),
                ref settings.IsLiverFirst);
            list.GapLine();

            /* ------------ 勾选框：是否自动改变用药方案 ------------- */
            list.CheckboxLabeled(
                "SmartHarvestOrganTax_Setting_IsChangeMedCare_Label".Translate(),
                ref settings.IsChangeMedCare);
            list.GapLine();

            /* ------------ 用药方案选择器（原生图标） --- */
            // 获取一行的矩形区域
            Rect rowRect = list.GetRect(30f);

            // 计算医疗选择器的理想尺寸
            float iconWidth = 32f;  // 每个图标的宽度
            float spacing = 5f;     // 图标之间的间距
            float totalIconsWidth = (iconWidth * 5) + (spacing * 4); // 5个图标，4个间隔

            // 计算标签的矩形区域（占用剩余空间）
            Rect labelRect = new Rect(
                rowRect.x,
                rowRect.y,
                rowRect.width - totalIconsWidth - 10f, // 10f是标签和图标之间的间距
                rowRect.height
            );

            // 计算医疗选择器的矩形区域
            Rect medCareRect = new Rect(
                rowRect.xMax - totalIconsWidth,
                rowRect.y + (rowRect.height - 30f) / 2, // 垂直居中
                totalIconsWidth,
                30f
            );

            // 绘制标签
            Widgets.Label(labelRect, "SmartHarvestOrganTax_Setting_MedicalCareSetter_Label".Translate());

            // 绘制医疗选择器
            MedicalCareUtility.MedicalCareSetter(medCareRect, ref settings.surgeryMedCare);

            list.End();
            base.DoSettingsWindowContents(inRect);
        }

    }
}