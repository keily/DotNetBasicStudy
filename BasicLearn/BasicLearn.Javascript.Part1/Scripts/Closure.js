/*
javascript闭包学习
*/

//当function里嵌套function时，内部的function可以访问外部function里的变量
function foo(a) {
    function spow(y) {
        return a << y;
    }
    return spow(2);
}
foo(5);

function foo1(a) {
    return function spow(y) {
        return a << y;
    }
}
f1 = foo1(4);
f1(2);

//闭包经常用于创建含有隐藏数据的函数
var dict = (function () {
    var data = {};
    return function (k, v) {
        if (v === undefined) {
            return data[k];
        } else {
            return data[k] = v;
        }
    }
})();

dict('x')
dict('x', 1)

//*************闭包uniqueID*************
uniqueID = (function () { //这个函数的调用对象保存值
    var id = 0; //这是私有恒久的那个值
    //外层函数返回一个有权访问恒久值的嵌套的函数,那就是我们保存在变量uniqueID里的嵌套函数.
    return function () { return id++; };  //返回,自加.
})(); //在定义后调用外层函数. 
console.log(uniqueID()); //0
console.log(uniqueID()); //1
console.log(uniqueID()); //2

//*************闭包阶乘*************
var a = (function (n) {
    if (n < 1) { alert("invalid arguments"); return 0; }
    if (n == 1) { return 1; }
    else { return n * arguments.callee(n - 1); }
})(4);
document.writeln(a);

function User(properties) {
    //这里一定要声明一个变量来指向当前的instance    
    var objthis = this;
    for (var i in properties) {
        (function () {
            //在闭包内，t每次都是新的，而 properties[i] 的值是for里面的    
            var t = properties[i];
            objthis["get" + i] = function () { return t; };
            objthis["set" + i] = function (val) { t = val; };
        })();
    }
}
//测试代码    
var user = new User({
    name: "Bob",
    age: 44
});
console.log(user.getname());
console.log(user.getage());
user.setname("Mike");
console.log(user.getname());
console.log(user.getage());
user.setage(22);
console.log(user.getname());
console.log(user.getage()); 


