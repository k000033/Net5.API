
using PX_WEL_BE_Lib.DAO.TableInfo;

namespace PX_WEL_BE_Lib.CURD.Merge
{
    public class SQL_Merge_Log
    {

        private TableName _tableName;


        public SQL_Merge_Log(TableName T)
        {

            this._tableName = T;
        }




        public string GetMERG_PersonnelInfoStr()
        {

            return @$"
            SET NOCOUNT ON;

            declare @update_table table(
	            [emp_id] [nvarchar](10) NOT NULL,
	            [emp_id_encry] [nvarchar](80) NULL,
	            [last_login_time] [datetime] NULL,
	            [last_ip] [nchar](15) NULL,
	            [click_upd_time] [datetime] NULL,
	            [emp_id_status] [bit] NULL
            );


            insert into @update_table  
            select [emp_id], [emp_id_encry], [last_login_time], [last_ip], [click_upd_time], [emp_id_status]
            from (values {{0}} ) as sub ( [emp_id], [emp_id_encry], [last_login_time], [last_ip], [click_upd_time], [emp_id_status]);


            merge into {this._tableName.PersonnelInfo} dest
            using (
                select * from @update_table
            ) src
            on (1=1
            and dest.emp_id = src.emp_id 
            )
            when MATCHED then
            update set
                emp_id_encry = src.emp_id_encry
	            , last_login_time = src.last_login_time
	            , last_ip = src.last_ip
	            , click_upd_time = src.click_upd_time
	            , emp_id_status = src.emp_id_status
            WHEN NOT MATCHED THEN 
            INSERT
            (
	            emp_id
	            , emp_id_encry
	            , last_login_time
	            , last_ip
	            , click_upd_time
	            , emp_id_status
            )
            VALUES
            (
	            src.emp_id
	            , src.emp_id_encry
	            , src.last_login_time
	            , src.last_ip
	            , src.click_upd_time
	            , src.emp_id_status
            );";

        }


    }
}
