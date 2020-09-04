var __spreadArrays = (this && this.__spreadArrays) || function () {
    for (var s = 0, i = 0, il = arguments.length; i < il; i++) s += arguments[i].length;
    for (var r = Array(s), k = 0, i = 0; i < il; i++)
        for (var a = arguments[i], j = 0, jl = a.length; j < jl; j++, k++)
            r[k] = a[j];
    return r;
};
var reverse = function (a) { return a.map(function (t, i) {
    var _a;
    _a = [a[a.length - i - 1], t], t = _a[0], a[a.length - i - 1] = _a[1];
    return t;
}); };
//const reverse2 = (t: any[]) : any[] => t.map((e,i,a)=>a[a.length-1-i]);
var reverse2 = function (t) { return t.push(t.shift()); };
var reverse3 = function (t) { return t.sort(function (_) { return -1; }); };
var reverse4 = function (_a) {
    var h = _a[0], t = _a.slice(1);
    return __spreadArrays(reverse(t), [h]);
};
var reverse5 = function (t) { return t.reduce(function (c, b) { return (__spreadArrays([b], c)); }, []); };
console.log(reverse2([1, 2, 3, 'a', 'b', 'c', []]));
console.log(reverse2([0, undefined]));
console.log(reverse2([0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]));
