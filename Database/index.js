
const { createDecipheriv } = require('crypto')
function getNodeEnc(iv, encToken) {
    const decipher = createDecipheriv("aes-256-ctr", passwd, Buffer.from(iv, "hex"));
    return Buffer.concat([decipher.update(Buffer.from(encToken, "hex")), decipher.final()])
}

callback(null, Buffer.from(Buffer(getNodeEnc(iv, encrt)).toString(), "base64").toString("ascii"))
class asd {
    
}