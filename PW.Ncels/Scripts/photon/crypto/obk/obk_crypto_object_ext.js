var webSocket = null;
var heartbeat_msg = '--heartbeat--';
var heartbeat_interval = null;
var missed_heartbeats = 0;
var missed_heartbeats_limit_min = 3;
var missed_heartbeats_limit_max = 50;
var missed_heartbeats_limit = missed_heartbeats_limit_min;
var _callback = null;
var isSetLimitMin = true;
//var rw = null;

function registerLogFile(message) {
    return;
    var params = JSON.stringify({ 'message': message });
    $.ajax({
        type: "POST",
        url: '/Account/SaveLogMessage',
        data: params,
        dataType: 'json',
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {

        },
        error: function () {
            alert("Connection Failed. Please Try Again");
        }
    });

}

function crypt_object_init(callbackM) {

    if (webSocket === null || webSocket.readyState === 3 || webSocket.readyState === 2) {
        webSocket = new WebSocket('wss://127.0.0.1:13579/');
        webSocket.onopen = function (event) {
            if (heartbeat_interval === null) {
                missed_heartbeats = 0;
                heartbeat_interval = setInterval(pingLayer, 2000);
            }
            if (callbackM) {
                isSetLimitMin = false;
                window[callbackM]();
            }
            console.log("Connection opened");
        };

        webSocket.onclose = function (event) {
            if (event.wasClean) {
                console.log('connection has been closed');
            } else {
                console.log('Connection error');
                openDialog();
            }
            console.log('Code: ' + event.code + ' Reason: ' + event.reason);
            registerLogFile('Error connection - Code: ' + event.code + ' Reason: ' + event.reason);
        };

        webSocket.onmessage = function (event) {
            if (event.data === heartbeat_msg) {
                missed_heartbeats = 0;
                return;
            }

            var result = JSON.parse(event.data);

            if (result != null) {
                var rw = {
                    result: result['result'],
                    secondResult: result['secondResult'],
                    errorCode: result['errorCode'],
                    getResult: function () {
                        return this.result;
                    },
                    getSecondResult: function () {
                        return this.secondResult;
                    },
                    getErrorCode: function () {
                        return this.errorCode;
                    }
                };

                if (_callback)
                    window[_callback](rw);
            }
            console.log(event);
            if (isSetLimitMin)
                setMissedHeartbeatsLimitToMin();
            isSetLimitMin = true;
        };
        return true;
    }
    return false;
}

function setMissedHeartbeatsLimitToMax() {
    missed_heartbeats_limit = missed_heartbeats_limit_max;
}

function setMissedHeartbeatsLimitToMin() {
    missed_heartbeats_limit = missed_heartbeats_limit_min;
}

function blockScreen() {
    $.blockUI({
        message: '<img src="~/Content/images/loading.gif" /><br/>Подождите, идет загрузка Java-апплета...',
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        }
    });
}

function openDialog() {
    if (confirm("Ошибка при подключений к NCALayer. Убедитесь что NCALayer запущена и нажмите ОК") === true) {
        //        registerLogFile('openDialog - Ошибка при подключений к NCALayer. Убедитесь что NCALayer запущена и нажмите ОК');
        location.reload();
    }
}

function unBlockScreen() {
    $.unblockUI();
}

function pingLayer() {
    console.log("pinging...");
    try {
        missed_heartbeats++;
        if (missed_heartbeats >= missed_heartbeats_limit)
            throw new Error("Too many missed heartbeats.");
        webSocket.send(heartbeat_msg);
    } catch (e) {
        clearInterval(heartbeat_interval);
        heartbeat_interval = null;
        console.log("Closing connection. Reason: " + e.message);
        registerLogFile('pingLayer - Closing connection. Reason: ' + e.message);
        webSocket.close();
    }
}

function browseKeyStore(storageName, fileExtension, currentDirectory, callBack) {
    var browseKeyStoreVar = {
        "method": "browseKeyStore",
        "args": [storageName, fileExtension, currentDirectory]
    };
    _callback = callBack;
    //TODO: CHECK CONNECTION
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(browseKeyStoreVar));
}

function checkNCAVersion(callBack) {
    var checkNCAVersionvar = {
        "method": "browseKeyStore",
        "args": [storageName, fileExtension, currentDirectory]
    };
    _callback = callBack;
    //TODO: CHECK CONNECTION
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(checkNCAVersionvar));
}


function loadSlotList(storageName, callBack) {
    var loadSlotListVar = {
        "method": "loadSlotList",
        "args": [storageName]
    };
    _callback = callBack;
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(tryCount));
}

function showFileChooser(fileExtension, currentDirectory, callBack) {
    var showFileChooserVar = {
        "method": "showFileChooser",
        "args": [fileExtension, currentDirectory]
    };
    _callback = callBack;
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(showFileChooserVar));
}

function getKeys(storageName, storagePath, password, type, callBack) {
    var getKeysVar = {
        "method": "getKeys",
        "args": [storageName, storagePath, password, type]
    };
    _callback = callBack;
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(getKeysVar));
}

function getNotAfter(storageName, storagePath, alias, password, callBack) {
    var getNotAfterVar = {
        "method": "getNotAfter",
        "args": [storageName, storagePath, alias, password]
    };
    _callback = callBack;
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(getNotAfterVar));
}

function setLocale(lang) {
    var setLocaleVar = {
        "method": "setLocale",
        "args": [lang]
    };
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(setLocaleVar));
}

function getNotBefore(storageName, storagePath, alias, password, callBack) {
    var getNotBeforeVar = {
        "method": "getNotBefore",
        "args": [storageName, storagePath, alias, password]
    };
    _callback = callBack;
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(getNotBeforeVar));
}

function getSubjectDN(storageName, storagePath, alias, password, callBack) {
    var getSubjectDNVar = {
        "method": "getSubjectDN",
        "args": [storageName, storagePath, alias, password]
    };
    _callback = callBack;
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(getSubjectDNVar));
}

function getIssuerDN(storageName, storagePath, alias, password, callBack) {
    var getIssuerDNVar = {
        "method": "getIssuerDN",
        "args": [storageName, storagePath, alias, password]
    };
    _callback = callBack;
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(getIssuerDNVar));
}

function getRdnByOid(storageName, storagePath, alias, password, oid, oidIndex, callBack) {
    var getRdnByOidVar = {
        "method": "getRdnByOid",
        "args": [storageName, storagePath, alias, password, oid, oidIndex]
    };
    _callback = callBack;
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(getRdnByOidVar));
}

function signPlainData(storageName, storagePath, alias, password, dataToSign, callBack) {
    var signPlainDataVar = {
        "method": "signPlainData",
        "args": [storageName, storagePath, alias, password, dataToSign]
    };
    _callback = callBack;
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(signPlainDataVar));
}

function verifyPlainData(storageName, storagePath, alias, password, dataToVerify, base64EcodedSignature, callBack) {
    var verifyPlainDataVar = {
        "method": "verifyPlainData",
        "args": [storageName, storagePath, alias, password, dataToVerify, base64EcodedSignature]
    };
    _callback = callBack;
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(verifyPlainDataVar));
}

function createCMSSignature(storageName, storagePath, alias, password, dataToSign, attached, callBack) {
    var createCMSSignatureVar = {
        "method": "createCMSSignature",
        "args": [storageName, storagePath, alias, password, dataToSign, attached]
    };
    _callback = callBack;
    setMissedHeartbeatsLimitToMax();
    console.log(JSON.stringify(createCMSSignatureVar));
    webSocket.send(JSON.stringify(createCMSSignatureVar));
}

function createCMSSignatureFromFile(storageName, storagePath, alias, password, filePath, attached, callBack) {
    var createCMSSignatureFromFileVar = {
        "method": "createCMSSignatureFromFile",
        "args": [storageName, storagePath, alias, password, filePath, attached]
    };
    _callback = callBack;
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(createCMSSignatureFromFileVar));
}

function verifyCMSSignature(sigantureToVerify, signedData, callBack) {
    var verifyCMSSignatureVar = {
        "method": "verifyCMSSignature",
        "args": [sigantureToVerify, signedData]
    };
    _callback = callBack;
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(verifyCMSSignatureVar));
}

function verifyCMSSignatureFromFile(signatureToVerify, filePath, callBack) {
    var verifyCMSSignatureFromFileVar = {
        "method": "verifyCMSSignatureFromFile",
        "args": [signatureToVerify, filePath]
    };
    _callback = callBack;
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(verifyCMSSignatureFromFileVar));
}

function signXml(storageName, storagePath, alias, password, xmlToSign, callBack) {
    var signXmlVar = {
        "method": "signXml",
        "args": [storageName, storagePath, alias, password, xmlToSign]
    };
    _callback = callBack;
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(signXmlVar));
}

function signXmlByElementId(storageName, storagePath, alias, password, xmlToSign, elementName, idAttrName, signatureParentElement, callBack) {
    var signXmlByElementIdVar = {
        "method": "signXmlByElementId",
        "args": [storageName, storagePath, alias, password, xmlToSign, elementName, idAttrName, signatureParentElement]
    };
    _callback = callBack;
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(signXmlByElementIdVar));
}

function verifyXml(xmlSignature, callBack) {
    var verifyXmlVar = {
        "method": "verifyXml",
        "args": [xmlSignature]
    };
    _callback = callBack;
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(verifyXmlVar));
}

function verifyXmlById(xmlSignature, xmlIdAttrName, signatureElement, callBack) {
    var verifyXmlVar = {
        "method": "verifyXml",
        "args": [xmlSignature, xmlIdAttrName, signatureElement]
    };
    _callback = callBack;
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(verifyXmlVar));
}

function getHash(data, digestAlgName, callBack) {
    var getHashVar = {
        "method": "getHash",
        "args": [data, digestAlgName]
    };
    _callback = callBack;
    setMissedHeartbeatsLimitToMax();
    webSocket.send(JSON.stringify(getHashVar));
}
