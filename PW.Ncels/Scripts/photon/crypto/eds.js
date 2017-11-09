var _storageAlias = "PKCS12";
String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.split(search).join(replacement);
};

function startSign(url, id, customDoSign) {
    _doSignCustom = customDoSign;
    $.blockUI({ message: '<h1><img src="../../Content/css/plugins/slick/ajax-loader.gif"/> Идет формирование отчета для подписи...</h1>', css: { opacity: 1 } });
    $.ajax({
        type: "POST",
        url: url,
        data: 'id=' + id,
        dataType: 'json',
        cache: false,
        success: function (data) {
            $.unblockUI();
            if (data.IsSuccess) {
                $("#hfXmlToSign").val(data.preambleXml);
                if (!crypt_object_init("chooseStoragePath")) {
                    chooseStoragePath();
                }
            }
        },
        error: function (data) {
            $.unblockUI();
        }
    });
}

function chooseStoragePath() {
    var storageAlias = _storageAlias;
    var storagePath = $("#hfStoragePath").val();
    if (storageAlias !== "NONE") {
        browseKeyStore(storageAlias, "P12", storagePath, "chooseStoragePathBack");
    }
}

function chooseStoragePathBack(rw) {
    var storagePath = $("#hfStoragePath").val();

    if (rw.getErrorCode() === "NONE" && rw.result) {
        debugger;
        $('#dlgPasswordModal').appendTo("body").modal();
        //$("#dlgPasswordModal").modal();
        storagePath = rw.getResult();
        if (storagePath) {
            $("#hfStoragePath").val(storagePath);
        }
        else {
            $("#hfStoragePath").val("");
        }
    } else {
        $("#hfStoragePath").val("");
    }
}

/**
* Дата выпуска эцп
*/
function getNotBeforeBack(result) {
    if (result['errorCode'] === "NONE") {
        $("#signDateFrom").text(result.result.split(' ')[0]);
        getNotAfterCall();
    }
    else {
        registerLogFile("eds.getNotBeforeBack result: " + result['result'] + '; code: ' + result['errorCode']);
        if (result['errorCode'] === "WRONG_PASSWORD" && result['result'] > -1) {
            alert("Неправильный пароль! Количество оставшихся попыток: " + result['result']);
        } else if (result['errorCode'] === "WRONG_PASSWORD") {
            alert("Неправильный пароль!");
        } else {
            alert(result['errorCode']);
        }
    }
}

function getNotBeforeCall() {
    var storageAlias = _storageAlias;
    var storagePath = $("#hfStoragePath").val();
    var password = $("#passwordCert").val();
    var alias = $("#hfKeyAlias").val();
    if (storagePath !== null && storagePath !== "" && storageAlias !== null && storageAlias !== "") {
        if (password !== null && password !== "") {
            if (alias !== null && alias !== "") {
                getNotBefore(storageAlias, storagePath, alias, password, "getNotBeforeBack");
            }
            else {
                alert("Вы не выбран ключ!");
            }
        } else {
            alert("Введите пароль к хранилищу");
        }
    } else {
        alert("Не выбран хранилище!");
    }
}

/**
* Дата окончания эцп
*/
function getNotAfterBack(result) {

    if (result['errorCode'] === "NONE") {
        $("#signDateTo").text(result.result.split(' ')[0]);
    } else {
        registerLogFile("eds.getNotAfterBack result: " + result['result'] + '; code: ' + result['errorCode']);
        if (result['errorCode'] === "WRONG_PASSWORD" && result['result'] > -1) {
            alert("Неправильный пароль! Количество оставшихся попыток: " + result['result']);
        } else if (result['errorCode'] === "WRONG_PASSWORD") {
            alert("Неправильный пароль!");
        } else {
            alert(result['errorCode']);
        }
    }
}

function getNotAfterCall() {
    var storageAlias = _storageAlias;
    var storagePath = $("#hfStoragePath").val();
    var password = $("#passwordCert").val();
    var alias = $("#hfKeyAlias").val();
    if (storagePath !== null && storagePath !== "" && storageAlias !== null && storageAlias !== "") {
        if (password !== null && password !== "") {
            if (alias !== null && alias !== "") {
                getNotAfter(storageAlias, storagePath, alias, password, "getNotAfterBack");
            } else {
                alert("Вы не выбрали ключ!");
            }
        } else {
            alert("Введите пароль к хранилищу");
        }
    } else {
        alert("Не выбран хранилище!");
    }
}


/**
 * @param  {object}
 * @param  {String}
 * @return {String|null}
 */
function findSubjectAttr(attrs, attr) {
    var tmp;
    var numb;

    for (numb = 0; numb < attrs.length; numb++) {
        tmp = attrs[numb].replace(/^\s\s*/, '').replace(/\s\s*$/, '');
        if (tmp.indexOf(attr + '=') === 0) {
            return tmp.substr(attr.length + 1);
        }
    }

    return null;
}

/**
 * Добавляем в html личные данные пользователя
 * @param  {Object}
 */
function fillPersonData(data) {
    var subjectDN = data.result;

    var subjectAttrs = subjectDN.split(',');
    var iin = findSubjectAttr(subjectAttrs, 'SERIALNUMBER').substr(3);
    var surename = findSubjectAttr(subjectAttrs, 'SURNAME');
    var cn = findSubjectAttr(subjectAttrs, 'CN');
    var g = findSubjectAttr(subjectAttrs, 'G');
    var email = findSubjectAttr(subjectAttrs, 'E');
    var obl = findSubjectAttr(subjectAttrs, 'L');
    SignUser(0, iin, null, cn, surename, obl, g, email);

    /*   document.getElementById('signIIN').innerHTML = iin;
       document.getElementById('signEmail').innerHTML = email;
       document.getElementById('signFIO').innerHTML = fullName;*/
}

/**
 * Дополнительные поля для организаций - БИН и наименование
 * @param  {Object}
 */
function fillOrgData(data) {

    var subjectAttrs = data.result.split(',');
    var bin = findSubjectAttr(subjectAttrs, 'OU').substr(3);
    var orgName = findSubjectAttr(subjectAttrs, 'O').replaceAll('\\\"', '\"');
    var cn = findSubjectAttr(subjectAttrs, 'CN');
    var surename = findSubjectAttr(subjectAttrs, 'SURNAME');
    var obl = findSubjectAttr(subjectAttrs, 'L');
    var g = findSubjectAttr(subjectAttrs, 'G');
    var email = findSubjectAttr(subjectAttrs, 'E');
    SignUser(1, bin, orgName, cn, surename, obl, g, email);

}

function SignUser(type, bin, orgName, cn, surename, obl, g, email) {


    var params = JSON.stringify({ 'type': type, 'bin': bin, 'orgName': orgName, 'cn': cn, 'surename': surename, 'obl': obl, 'g': g, 'email': email });
    registerLogFile(params);

    window.location = '/Account/RegisterEcp?type=' +
        type +
        '&bin=' +
        bin +
        "&orgName=" +
        orgName +
        '&cn=' +
        cn +
        '&surename=' +
        surename +
        '&obl=' +
        obl +
        '&g=' +
        g +
        '&email=' +
        email;
}

function getSubDNCallBack(result) {

    if (result.errorCode === 'NONE') {
        var subjectAttrs = result.result.split(',');
        var bin = findSubjectAttr(subjectAttrs, 'OU');
        if (bin != null && bin.length > 0) {
            fillOrgData(result);
        } else {
            fillPersonData(result);
        }
        ///var orgName = findSubjectAttr(subjectAttrs, 'O');




        getNotBeforeCall();
    } else {
        registerLogFile("eds.getSubDNCallBack result: " + result['result'] + '; code: ' + result.errorCode);
        //openNcaLayerError();
    }
}

function getSubDNCall() {
    var storageAlias = _storageAlias;
    var storagePath = $("#hfStoragePath").val();
    var password = $('#passwordCert').val();
    var alias = $("#hfKeyAlias").val();
    if (storagePath !== null && storagePath !== "" && storageAlias !== null && storageAlias !== "") {
        if (password !== null && password !== "") {
            if (alias !== null && alias !== "") {
                getSubjectDN(storageAlias, storagePath, alias, password, "getSubDNCallBack");
            } else {
                alert("Вы не выбрали ключ!");
            }
        } else {
            alert("Введите пароль к хранилищу");
        }
    } else {
        alert("Не выбран хранилище!");
    }
}

/**
 * @param  {[type]}
 * @param  {[type]}
 * @return {Boolean}
 */
var _submitCallback = null;

function signXmlBack(result) { // eslint-disable-line
    var signedData;
    if (result.errorCode === 'NONE') {
        signedData = result.getResult();
        $('#Certificate').val(signedData);
        // webSocket.close();

        if (_submitCallback)
            _submitCallback();
    }
    else
        if (result.errorCode === 'WRONG_PASSWORD' && result.result > -1) {
        } else if (result.errorCode === 'WRONG_PASSWORD') {
        } else {
            $('#Certificate').val('');
        }
}

/**
 * Вызываем подписку через прослойку
 *
 * @return {callback}
 */
function signXmlCall(submitCallback) {
    _submitCallback = submitCallback;
    var data = $('#hfXmlToSign').val();
    var storageAlias = _storageAlias;
    var storagePath = $('#hfStoragePath').val();
    var password = $('#passwordCert').val();
    var alias = $('#hfKeyAlias').val();
    var args = [];
    var numb;

    if (storagePath && storageAlias && password && alias && data) {
        args = [storageAlias, storagePath, alias, password, data];
        // getData('signXml', args, callbackM);
        signXml(storageAlias, storagePath, alias, password, data, "signXmlBack");

    } else {
        // openNcaLayerError();
    }
}

function fillKeysBack(result) {

    if (result['errorCode'] === "NONE") {
        var list = result['result'];
        var slotListArr = list.split("\n");
        for (var i = 0; i < slotListArr.length; i++) {
            if (slotListArr[i] === null || slotListArr[i] === "") {
                continue;
            }
            var str = slotListArr[i];
            var alias = str.split("|")[3];
            $("#hfKeyAlias").val(alias);
            getSubDNCall();
            break;
        }
    }
    else {
        registerLogFile("eds.fillKeysBack result: " + result['result'] + '; code: ' + result['errorCode']);
        if (result['errorCode'] === "WRONG_PASSWORD" && result['result'] > -1) {
            alert("Неправильный пароль! Количество оставшихся попыток: " + result['result']);
        } else if (result['errorCode'] === "WRONG_PASSWORD") {
            alert("Неправильный пароль!");
        } else {
            alert(result['errorCode']);
        }
    }
}

function fillKeys() {

    var storageAlias = _storageAlias;
    var storagePath = $("#hfStoragePath").val();
    var password = $('#passwordCert').val();
    //var password = passwordP;
    // AUTH 
    // SIGN
    // ALL
    var keyType = "AUTH";

    if (storagePath && storageAlias) {
        if (password) {
            getKeys(storageAlias, storagePath, password, keyType, "fillKeysBack");
        } else {
            alert("Введите пароль к хранилищу");
        }
    } else {
        alert("Не выбран хранилище!");
    }
}
var _doSign = null;
var _doSignCustom = null;
function fillKeySign(doSign) {
    _doSign = doSign;
    var storageAlias = _storageAlias;
    var storagePath = $("#hfStoragePath").val();
    var password = $('#passwordCert').val();
    //var password = passwordP;
    // AUTH 
    // SIGN
    // ALL
    var keyType = "SIGN";

    if (storagePath && storageAlias) {
        if (password) {
            getKeys(storageAlias, storagePath, password, keyType, "fillKeysSignBack");
        } else {
            alert("Введите пароль к хранилищу");
        }
    } else {
        alert("Не выбран хранилище!");
    }
}

function fillKeysSignBack(result) {

    if (result['errorCode'] === "NONE") {
        var list = result['result'];
        var slotListArr = list.split("\n");
        for (var i = 0; i < slotListArr.length; i++) {
            if (slotListArr[i] === null || slotListArr[i] === "") {
                continue;
            }
            var str = slotListArr[i];
            var alias = str.split("|")[3];
            $("#hfKeyAlias").val(alias);
            debugger;
            if (_doSignCustom) {
                _doSignCustom();
            }
            else if (_doSign)
                _doSign();
            break;
        }
    }
    else {
        registerLogFile("eds.fillKeysSignBack result: " + result['result'] + '; code: ' + result['errorCode']);
        if (result['errorCode'] === "WRONG_PASSWORD" && result['result'] > -1) {
            alert("Неправильный пароль! Количество оставшихся попыток: " + result['result']);
        } else if (result['errorCode'] === "WRONG_PASSWORD") {
            alert("Неправильный пароль!");
        } else {
            alert(result['errorCode']);
        }
    }
}