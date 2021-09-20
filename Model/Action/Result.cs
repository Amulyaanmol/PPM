using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model.Action
{
    public class ActionResult
    {
        public bool IsPositiveResult {  get; set; }
        public string Message {  get; set; }
    }
    public class DataResults<T> : ActionResult { public IEnumerable<T> results; }
}
