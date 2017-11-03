using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using MiocroServicioPagoefectivoMailPost;

namespace MiocroServicioPagoefectivoMailPost.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestFunctionHandler()
        {
            try
            {

                LoadEnvironmentVariable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }

        private void LoadEnvironmentVariable()
        {
            throw new NotImplementedException();
        }
    }
}
