function ArrayContains(array, obj) {
    for (var i = 0; i < array.length; i++) {
        if (array[i] === obj) {
            return true;
        }
    }
    return false;
}


function RemoveArray(array, attachId) {
    var f = false;
    for (var i = 0, n = 0; i < array.length; i++) {
        if (array[i] != attachId) {
            array[n++] = array[i]
        } else
            f = true;
    }
    if (f == true)
        array.length -= 1;
}

Array.prototype.remove = function (obj) {
    return RemoveArray(this, obj);
};

Array.prototype.contains = function (obj) {
    return ArrayContains(this, obj);
};