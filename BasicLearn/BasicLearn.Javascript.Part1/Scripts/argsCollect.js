function format(string) {
    var args = arguments;
    //"%([1-" + arguments.length-1 + "])" will be throw exception
    //cause "" is string object, x.length is number object,string + number '+' operator be override join operator,return a string object
    //then string object - "" string object,'-' donot be override,throw a NaN exception
    var pattern = new RegExp("%([1-" + (arguments.length-1) + "])", "g");
    return String(string).replace(pattern, function (match, index) {
        return args[index];
    });
};

function makeFunc() {
    var args = Array.prototype.slice.call(arguments);
    //get a arguments 
    var func = args.shift();
    return function () {
        return func.apply(null, args.concat(Array.prototype.slice.call(arguments)));
    };
}
//第一个数字表示运行的次数，而第二个函数定义运行的间隔时间（毫秒为单位）
function repeat(fn, times, delay) {
    return function () {
        if (times-- > 0) {
            fn.apply(null, arguments);
            var args = Array.prototype.slice.call(arguments);
            var self = arguments.callee;
            setTimeout(function () { self.apply(null, args) }, delay);
        }
    };
}
function comms(s) { alert(s); }

$(function () {
    debugger;
    var aformat = format("And the %1 want to know whose %2 you %3", "papers", "shirt", "wear");
    var majorTom = makeFunc(format, "This is Major Tom to ground control. I'm %1.");

    //重复三次运行每次间隔两秒
    //var somethingWrong = repeat(comms, 3, 2000);
    //somethingWrong("Can you hear me, major tom?");

    console.log(aformat);
});