
function esEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

function mensaje(mensaje) {

    $("#contenidoMensaje").text(mensaje);
    OpenMensaje();
}

function OpenMensaje() {
    $("#modalInfo").modal('show');
}

function CloseMensaje() {
    $("#modalInfo").modal('hide');
}

function RutValido(rut) {
    return CheckRutField_tmp(rut);
}

function DoClear(theText) {
    if (theText.value == theText.defaultValue) {
        theText.value = "";
    }
}

function FormatRut(objRut) {
    if (objRut.value != '') {
        var rut_val = objRut.value.toUpperCase();
        var largo = rut_val.length;
        var tmpstr = "";

        for (i = 0; i < rut_val.length; i++) {
            if (rut_val.charAt(i) != ' ' && rut_val.charAt(i) != '.' && rut_val.charAt(i) != '-') {
                tmpstr = tmpstr + rut_val.charAt(i);
            }
        }
        rut_val = tmpstr;
        rut_valor = rut_val.substring(0, rut_val.length - 1);


        var invertido = "";
        for (i = (rut_val.length - 1), j = 0; i >= 0; i--, j++)
            invertido = invertido + rut_val.charAt(i);
        var drut = "";
        drut = drut + invertido.charAt(0);
        drut = drut + '-';
        cnt = 0;
        for (i = 1, j = 2; i < largo; i++, j++) {
            if (cnt == 3) {
                drut = drut + '.';
                j++;
                drut = drut + invertido.charAt(i);
                cnt = 1;
            }
            else {
                drut = drut + invertido.charAt(i);
                cnt++;
            }
        }

        invertido = "";
        for (i = (drut.length - 1), j = 0; i >= 0; i--, j++)
            invertido = invertido + drut.charAt(i);

        if (invertido.charAt(0) == '.') {
            invertido = invertido.substring(1, invertido.length);
        }

        objRut.value = invertido;

    }
}

function CheckRutField_tmp(texto) {
    var tmpstr = "";
    var tmpstr2 = "";
    for (i = 0; i < texto.length; i++)
        if (texto.charAt(i) != ' ' && texto.charAt(i) != '.' && texto.charAt(i) != '-')
            tmpstr = tmpstr + texto.charAt(i);
    texto = tmpstr;
    largo = texto.length;

    if (largo < 2) {
        return false
    }
    for (i = 0; i < largo; i++) {
        if (texto.charAt(i) != "0" && texto.charAt(i) != "1" && texto.charAt(i) != "2" && texto.charAt(i) != "3" && texto.charAt(i) != "4" && texto.charAt(i) != "5" && texto.charAt(i) != "6" && texto.charAt(i) != "7" && texto.charAt(i) != "8" && texto.charAt(i) != "9" && texto.charAt(i) != "k" && texto.charAt(i) != "K") {
            return false
        }
    }

    var invertido = "";
    for (i = (largo - 1), j = 0; i >= 0; i--, j++)
        invertido = invertido + texto.charAt(i);

    var dtexto = "";
    dtexto = dtexto + invertido.charAt(0);
    dtexto = dtexto + '-';
    cnt = 0;

    for (i = 1, j = 2; i < largo; i++, j++) {
        if (cnt == 3) {
            dtexto = dtexto + '.';
            j++;
            dtexto = dtexto + invertido.charAt(i);
            cnt = 1
        }
        else {
            dtexto = dtexto + invertido.charAt(i);
            cnt++
        }
    }

    invertido = "";
    for (i = (dtexto.length - 1), j = 0; i >= 0; i--, j++)
        invertido = invertido + dtexto.charAt(i);

    if (checkDV_tmp(texto)) {
        return true
    }
    return false;

}

function checkDV_tmp(crut) {
    largo = crut.length;
    if (largo < 2) {
        return false
    }

    if (largo > 2)
        rut = crut.substring(0, largo - 1);
    else
        rut = crut.charAt(0);
    dv = crut.charAt(largo - 1);
    checkCDV(dv);

    if (rut == null || dv == null) { return 0 }

    var dvr = '0';

    suma = 0;
    mul = 2;

    for (i = rut.length - 1; i >= 0; i--) {
        suma = suma + rut.charAt(i) * mul;
        if (mul == 7) { mul = 2 }
        else { mul++ }
    }


    res = suma % 11;
    if (res == 1) { dvr = 'k' }
    else if (res == 0) { dvr = '0' }
    else {
        dvi = 11 - res;
        dvr = dvi + "";
    }

    if (dvr != dv.toLowerCase()) {
        return false
    }
    return true;
}

function checkCDV(dvr) {
    dv = dvr + "";
    if (dv != '0' && dv != '1' && dv != '2' && dv != '3' && dv != '4' && dv != '5' && dv != '6' && dv != '7' && dv != '8' && dv != '9' && dv != 'k' && dv != 'K') {
        PopUpMensaje("Error", "Debe ingresar un digito verificador valido.");
        document.frm.txtRUT.focus();
        document.frm.txtRUT.select();
        return false;
    }
    return true;
}

function soloCaracteresRut(e) {
    var key = window.Event ? e.which : e.keyCode
    return (key >= 48 && key <= 57 || key == 75 || key == 107 || key == 46)
}

function soloCaracteresHora(e) {
    var key = window.Event ? e.which : e.keyCode
    return (key >= 48 && key <= 58)
}

function soloCaracteresMinutos(e) {
    var key = window.Event ? e.which : e.keyCode
    return (key >= 48 && key <= 57)
}

function soloCaracteresNumeros(e) {
    var key = window.Event ? e.which : e.keyCode
    return (key >= 48 && key <= 57)
}