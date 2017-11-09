var _storageAlias = "PKCS12";
String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.split(search).join(replacement);
};

function startSign(xmlData, docId, saveSignCallback) {
    debugger;
    var window = $("#DigSignWindow");
    window.kendoWindow({
        width: "650",
        height: "auto",
        title: 'Подписание',
        visible: false,
        modal: true
    });
    window.data("kendoWindow").unbind();
    window.data("kendoWindow").bind("refresh", function () {
        debugger;
        $('#hfXmlToSign' + docId).val(xmlData);
        $("#hfStoragePath" + docId).val(window.data("kendoWindow").certStoragePath);
        $("#signDoc" + docId).bind("click",
            function () {
                signXmlCall(saveSignCallback, docId);
                $("#DigSignWindow").data("kendoWindow").close();
            });
        $("#cancelSign" + docId).bind("click",
            function () {
                $("#DigSignWindow").data("kendoWindow").close();
            });        
        $("#loadKeys" + docId).bind("click",
            function () {
                fillKeySign(docId);
            });
    });
    if (!crypt_object_init("chooseStoragePath", docId)) {
        chooseStoragePath(docId);
    }
}

function chooseStoragePath(uiId) {
    var storageAlias = _storageAlias;
    var storagePath = $("#hfStoragePath" + uiId).val();
    if (storageAlias !== "NONE") {
        browseKeyStore(storageAlias, "P12", storagePath, "chooseStoragePathBack", uiId);
    }
}

function chooseStoragePathBack(rw, uiId) {
    debugger;   
    if (rw.getErrorCode() === "NONE" && rw.result) {
        debugger;
        var storagePath = rw.getResult();
        if (storagePath) {
            var window = $("#DigSignWindow");
            window.data("kendoWindow").refresh(
                {
                    url: "/Home/DigSign",
                    data: { documentId: uiId }
                });
            window.data("kendoWindow").title('Подписание');
            window.data("kendoWindow").setOptions({
                width: "650",
                height: "auto",
                title: 'Подписание'
            });
            window.data("kendoWindow").center();
            window.data("kendoWindow").open();
            window.data("kendoWindow").certStoragePath = storagePath;
        }
        else {
            $("#hfStoragePath" + uiId).val("");
        }
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

function signXmlBack(result, uiId) { // eslint-disable-line
    debugger;
    var signedData;
    if (result.errorCode === 'NONE') {
        signedData = result.getResult();

        if (_submitCallback)
            _submitCallback(signedData, uiId);
    }
    else
        if (result.errorCode === 'WRONG_PASSWORD' && result.result > -1) {
        } else if (result.errorCode === 'WRONG_PASSWORD') {
        } else {            
        }
}

/**
 * Вызываем подписку через прослойку
 *
 * @return {callback}
 */
function signXmlCall(submitCallback, uiId) {
    debugger;
    _submitCallback = submitCallback;
    var data = $('#hfXmlToSign' + uiId).val();
    var storageAlias = _storageAlias;
    var storagePath = $('#hfStoragePath' + uiId).val();
    var password = $('#passwordCert' + uiId).val();
    var aliasVal = $('#keysList' + uiId +" option:selected").text();
    var alias = aliasVal.split("|")[3];

    if (storagePath && storageAlias && password && alias && data) {
        signXml(storageAlias, storagePath, alias, password, data, "signXmlBack", uiId);

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

function fillKeySign(uiId) {
    var storageAlias = _storageAlias;
    var storagePath = $("#hfStoragePath" + uiId).val();
    var password = $('#passwordCert' + uiId).val();
    var keyType = "SIGN";

    if (storagePath && storageAlias) {
        if (password) {
            getKeys(storageAlias, storagePath, password, keyType, "fillKeysSignBack", uiId);
        } else {
            alert("Введите пароль к хранилищу");
        }
    } else {
        alert("Не выбран хранилище!");
    }
}

function fillKeysSignBack(result, uiId) {
    debugger;
    var keyListEl = document.getElementById('keysList' + uiId);
    keyListEl.options.length = 0;
    if (result['errorCode'] === "NONE") {
        var list = result['result'];
        var slotListArr = list.split("\n");
        for (var i = 0; i < slotListArr.length; i++) {
            if (slotListArr[i] === null || slotListArr[i] === "") {
                continue;
            }
            keyListEl.options[keyListEl.length] = new Option(slotListArr[i], i);
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