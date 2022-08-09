using System;

namespace DbModels
{
    /// <summary>
    /// 物料表 panel或者panel cell
    /// </summary>
    public class R_KPSN_T
    {

        /// <summary>
        /// 
        /// </summary>
        public string GID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LAST_PALLET_GID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LAST_PALLET_NO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LAST_LOT_GID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LAST_LOT_NO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LAST_BLOCK_GID { get; set; }

        /// <summary>
        /// Block or not, 1: block; 0: unblock;  null: no block
        /// </summary>
        public decimal? IS_BLOCK { get; set; }

        /// <summary>
        /// IQC lot result:: 1 for receive, 0 if IQC lot result OK,  1 if not OK
        /// </summary>
        public decimal? IS_IQC_BLOCK { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string KPSN { get; set; }

        /// <summary>
        /// select Value from pts_ap.c_dictionary_t where type = 'KPJOB_STATE'
        /// </summary>
        public string KP_TYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PART_NO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ORDER_TYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ORDER_NO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ORDER_ITEM { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CREATE_EMP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CREATE_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string JOB_STATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UPDATE_EMP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? UPDATE_DATE { get; set; }

        /// <summary>
        /// UTC of UPDATE_DATE
        /// </summary>
        public DateTime? UTC_TIME { get; set; }

        /// <summary>
        /// updated by SFIS
        /// </summary>
        public string LCMSN { get; set; }

        /// <summary>
        /// updated by SFIS
        /// </summary>
        public string LCM_PN { get; set; }

        /// <summary>
        /// updated by SFIS
        /// </summary>
        public string LCM_MO { get; set; }

        /// <summary>
        /// updated by SFIS
        /// </summary>
        public DateTime? LCM_ASSY_UTC { get; set; }

        /// <summary>
        /// updated by SFIS
        /// </summary>
        public string LCM_ASSY_SITE { get; set; }

        /// <summary>
        /// updated by SFIS
        /// </summary>
        public string LCM_ASSY_LINE { get; set; }

        /// <summary>
        /// updated by SFIS
        /// </summary>
        public string FGSN { get; set; }

        /// <summary>
        /// updated by SFIS
        /// </summary>
        public string FG_PN { get; set; }

        /// <summary>
        /// updated by SFIS
        /// </summary>
        public string FG_MO { get; set; }

        /// <summary>
        /// updated by SFIS
        /// </summary>
        public DateTime? FG_ASSY_UTC { get; set; }

        /// <summary>
        /// updated by SFIS
        /// </summary>
        public string FG_ASSY_SITE { get; set; }

        /// <summary>
        /// updated by SFIS
        /// </summary>
        public string FG_ASSY_LINE { get; set; }

        /// <summary>
        /// updated by SFIS
        /// </summary>
        public string FG_CUSTOMER { get; set; }

        /// <summary>
        /// updated by SFIS
        /// </summary>
        public string FG_CUSTMODEL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SITE { get; set; }

        /// <summary>
        /// 1: in WH; 0: out of WH
        /// </summary>
        public decimal? WH_STATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WARE_CODE { get; set; }

        /// <summary>
        /// Reserved
        /// </summary>
        public string FACTORY_CODE { get; set; }

        /// <summary>
        /// updated when Transfer-to-Fab, by PTS or FPW/WMS
        /// </summary>
        public string IS_THROW { get; set; }

        /// <summary>
        /// updated by PTS TimeTask
        /// </summary>
        public string IS_THROW_SUCC { get; set; }

        /// <summary>
        /// updated by PTS TimeTask
        /// </summary>
        public DateTime? THROW_SUCC_DATE { get; set; }

        /// <summary>
        /// updated when Transfer-to-Fab, by PTS or FPW/WMS
        /// </summary>
        public string REMOVE_NO { get; set; }

        /// <summary>
        /// updated when Transfer-to-Fab, by PTS or FPW/WMS
        /// </summary>
        public string REMOVE_ITEM { get; set; }

        /// <summary>
        /// updated when Transfer-to-Fab, by PTS or FPW/WMS
        /// </summary>
        public string REMOVE_SITE { get; set; }

        /// <summary>
        /// 1: OC, 2: Module, 3: LCM WIP, 4: LCM, 5: FG WIP, 6: FG
        /// </summary>
        public string PROD_STATUS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PROD_LOCATION { get; set; }

        /// <summary>
        /// default 1: in TPVGroup or D/C or O/SFab, 2 TPVGroup(CS/RD/QD..), 3 out to customer or Scrap,market,rma
        /// </summary>
        public string FINISH_FLAG { get; set; }

        /// <summary>
        /// update at PTS trigger.  in which batch this SN is to be send;
        /// </summary>
        public string BATCH_ID { get; set; }

        /// <summary>
        /// update at PTS trigger.  Seq_no start from 1, every update incease by 1
        /// </summary>
        public decimal? SEQ_NO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RECV_SITE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RECV_PO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RECV_POITEM { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RECV_KPTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? RECV_UTC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OC_SN { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OC_PN { get; set; }

        /// <summary>
        /// updated by FPW/WMS
        /// </summary>
        public DateTime? LCM_WH_IN_UTC { get; set; }

        /// <summary>
        /// updated by FPW/WMS
        /// </summary>
        public DateTime? LCM_WH_OUT_UTC { get; set; }

        /// <summary>
        /// updated by FPW/WMS, 同时也为LCM的DN1
        /// </summary>
        public string LCM_WH_SHIPNO { get; set; }

        /// <summary>
        /// updated by FPW/WMS
        /// </summary>
        public string LCM_WH_SHIPTO { get; set; }

        /// <summary>
        /// updated by FPW/WMS
        /// </summary>
        public DateTime? FG_WH_IN_UTC { get; set; }

        /// <summary>
        /// updated by FPW/WMS
        /// </summary>
        public DateTime? FG_WH_OUT_UTC { get; set; }

        /// <summary>
        /// updated by FPW/WMS, 同时也为FG的DN1
        /// </summary>
        public string FG_WH_SHIPNO { get; set; }

        /// <summary>
        /// updated by FPW/WMS
        /// </summary>
        public string FG_WH_SHIPTO { get; set; }

        /// <summary>
        /// updated by FPW/WMS
        /// </summary>
        public DateTime? DC_WH_IN_UTC { get; set; }

        /// <summary>
        /// updated by FPW/WMS
        /// </summary>
        public DateTime? DC_WH_OUT_UTC { get; set; }

        /// <summary>
        /// updated by FPW/WMS
        /// </summary>
        public string DC_WH_SHIPNO { get; set; }

        /// <summary>
        /// updated by FPW/WMS
        /// </summary>
        public string DC_WH_SHIPTO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? SYNC_CENTER_UTC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RECV_IDN { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RECV_SAPCLIENT { get; set; }

        /// <summary>
        /// if 1: ignore once for inserting r_kpsn_History_t in trigger; else need
        /// </summary>
        public decimal? IGNORE_TRIGGER { get; set; }

        /// <summary>
        /// R: R-Area OldStock; W: MaterialWH OldStock; null: not oldstock; 
        /// </summary>
        public string OLD_STOCK { get; set; }

        /// <summary>
        /// Box code
        /// </summary>
        public string CARTON_NO { get; set; }

        /// <summary>
        /// 首收料号
        /// </summary>
        public string RECV_PARTNO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RECV_LOTGID { get; set; }

        /// <summary>
        /// 当前料号
        /// </summary>
        public string CURRENT_PARTNO { get; set; }

        /// <summary>
        /// Size
        /// </summary>
        public string PN_SIZE { get; set; }

        /// <summary>
        /// 公司代号
        /// </summary>
        public string COMPANY_CODE { get; set; }

        /// <summary>
        /// 年月
        /// </summary>
        public string YEAR_MONTH { get; set; }

        /// <summary>
        /// T1开始时间
        /// </summary>
        public DateTime? T1_STARTDATE { get; set; }

        /// <summary>
        /// T2开始时间
        /// </summary>
        public DateTime? T2_STARTDATE { get; set; }

        /// <summary>
        /// T3开始时间
        /// </summary>
        public DateTime? T3_STARTDATE { get; set; }

        /// <summary>
        /// T4开始时间
        /// </summary>
        public DateTime? T4_STARTDATE { get; set; }

        /// <summary>
        /// T5开始时间
        /// </summary>
        public DateTime? T5_STARTDATE { get; set; }

        /// <summary>
        /// T1结束时间
        /// </summary>
        public DateTime? T1_ENDDATE { get; set; }

        /// <summary>
        /// T2结束时间
        /// </summary>
        public DateTime? T2_ENDDATE { get; set; }

        /// <summary>
        /// T3结束时间
        /// </summary>
        public DateTime? T3_ENDDATE { get; set; }

        /// <summary>
        /// T4结束时间
        /// </summary>
        public DateTime? T4_ENDDATE { get; set; }

        /// <summary>
        /// T5结束时间
        /// </summary>
        public DateTime? T5_ENDDATE { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public string CURRENT_STATUS { get; set; }

        /// <summary>
        /// 是否结束
        /// </summary>
        public decimal? IS_END { get; set; }

        /// <summary>
        /// 单位成本价格
        /// </summary>
        public double? PN_PRICE { get; set; }

        /// <summary>
        /// PO价格
        /// </summary>
        public double? PO_PRICE { get; set; }

        /// <summary>
        /// 当前形态 from SFIS
        /// </summary>
        public string CURRENT_KPTYPE { get; set; }

        /// <summary>
        /// for PO/IDN, seach WU_PO, WU_POITEM, DeliveryDate from R_LOT_T (orignal from R_LOGISTICS_PART_T from Center from SAP)
        /// </summary>
        public string WU_PO { get; set; }

        /// <summary>
        /// for PO/IDN, seach WU_PO, WU_POITEM, DeliveryDate from R_LOT_T (orignal from R_LOGISTICS_PART_T from Center from SAP)
        /// </summary>
        public string WU_ITEM { get; set; }

        /// <summary>
        /// 1: delete after receive; 0: na   DEL_FLAG
        /// </summary>
        public decimal? DEL_FLAG { get; set; }

        /// <summary>
        /// updated by FPW/WMS NEW BARCODE.R_DELIVERY_NOTICE_T.SUB_BG_DESC
        /// </summary>
        public string LCM_WH_CUST { get; set; }

        /// <summary>
        /// updated by FPW/WMS NEW BARCODE.R_DELIVERY_NOTICE_T.SUB_BG_DESC
        /// </summary>
        public string FG_WH_CUST { get; set; }
    }
}