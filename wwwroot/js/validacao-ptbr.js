$.validator.methods.number = function (value, element) {
    if (this.optional(element)) {
        return true;
    }

    return /^-?(?:\d+|\d{1,3}(?:[\s.,]\d{3})+)(?:[.,]\d+)?$/.test(value);
};

$.validator.methods.range = function (value, element, param) {

    if (this.optional(element)) {
        return true;
    }

    var numero = parseFloat(value.replace(",", "."));

    return numero >= param[0] && numero <= param[1];
};