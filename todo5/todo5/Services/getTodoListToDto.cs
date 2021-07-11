using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using todo5.DTO;

namespace todo5.Services
{
    public class getTodoListToDto
    {

        public IEnumerable<todoLostGetDTO> todoLostGetDTO(DataTable dt)
        {
            var uploadFiles = dt.AsEnumerable().Select(x => new UploadFileDTO
            {
                Name = x.Field<string>("File_Name"),
                Src = x.Field<string>("File_Src"),
                TodoId = x.Field<Guid?>("FILE_TodoId")
            });

           
            return dt.AsEnumerable().Select(x => new todoLostGetDTO
            {
                Name = x.Field<string>("Name"),
                Enable = x.Field<bool>("Enable"),
                Orders = x.Field<int>("Orders"),
                InsertTime = x.Field<DateTime>("InsertTime"),
                UpdateTime = x.Field<DateTime>("UpdateTime"),
                InsertEmployeeName = x.Field<String>("InsertEmployeeName"),
                UpdateEmployeeName = x.Field<String>("UpdateEmployeeName"),
                uploadFileDTOs = uploadFiles.Where(y => y.TodoId == x.Field<Guid>("TodoId"))
            });
        }
    }
}
