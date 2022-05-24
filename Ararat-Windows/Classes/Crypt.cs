using Jering.Javascript.NodeJS;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXCLIENT.Classes
{
    class Crypt
    {
        INodeJSService nodeService;
        Crypt()
        {
            var services = new ServiceCollection();
            services.AddNodeJS();
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            nodeService = serviceProvider.GetRequiredService<INodeJSService>();
        }
        private async Task<string> Decrypt(string key, string iv, string encrypted)
        {
            string result = await nodeService.InvokeFromStringAsync<string>(
                               "module.exports = (callback, passwd, iv, encrt) => {" +
                               "const { createDecipheriv } = require('crypto');" +
                               "function getNodeEnc(iv, encToken){" +
                                  "const decipher = createDecipheriv('aes-256-ctr', passwd, Buffer.from(iv, 'hex'));" +
                                  "return Buffer.concat([decipher.update(Buffer.from(encToken, 'hex')), decipher.final()]);" +
                               "};" +
                               "callback(null, Buffer.from(Buffer(getNodeEnc(iv, encrt)).toString(), 'base64').toString('ascii'));}"
                    , args: new[] { key, iv, encrypted });
            return result;
        }
    }
}
