using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.ExceptionHandler.ResponseWrapper.Wrappers
{
    public class PetitionWrapper<T>
    {
        public PetitionWrapper()
        {

        }
        public PetitionWrapper(T data)
        {
            Data = data;
        }
        //[Required(ErrorMessage = "Debe proveer el código de aplicación")]
        public string AppCode { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Debe proveer los datos de la petición")]
        public T Data { get; set; }
        public DateTime ReceivedDate { get; set; } = DateTime.Now;

    }
}
