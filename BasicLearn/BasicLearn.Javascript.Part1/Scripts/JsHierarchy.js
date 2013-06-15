/*
js继承有5种实现方式： 
1、继承第一种方式：对象冒充 
*/
function Parent(username) {
    this.username = username;
    this.hello = function () {console.log(this.username);}
}
function Child(username, password) {
    //通过以下3行实现将Parent的属性和方法追加到Child中，从而实现继承 
    //第一步：this.method是作为一个临时的属性，并且指向Parent所指向的对象， 
    //第二步：执行this.method方法，即执行Parent所指向的对象函数 
    //第三步：销毁this.method属性，即此时Child就已经拥有了Parent的所有属性和方法
    this.method = Parent;
    this.method(username);
    delete this.method;
    this.password = password;
    this.world = function () {
        var plog = { username: this.username, psw: this.password };
        console.log(plog);
    }
}
var parent1=new Parent('keily');
var child1=new Child('rollng','123');
parent1.hello();
child1.hello();
child1.world();

/*
2、继承第二种方式：call()方法方式 
  call方法是Function类中的方法 
  call方法的第一个参数的值赋值给类(即方法)中出现的this 
  call方法的第二个参数开始依次赋值给类(即方法)所接受的参数 
*/
function Parent2(username) {
    this.username = username;
    this.hello = function () { console.log(this.username); }
}
function Child2(username, password) {
    Parent2.call(this, username);
    //call=apply 只是构造函数的参数是一个数组而已
    //Parent2.apply(this, new Array(username));
    this.password = password;
    this.world = function () {
        var plog = { username: this.username, psw: this.password };
        console.log( plog);
    }
}
var parent2 = new Parent2('keily');
var child2 = new Child2('rollng', '123');
parent2.hello();
child2.hello();
child2.world();

/*
4、继承的第四种方式：原型链方式，即子类通过prototype将所有在父类中
通过prototype追加的属性和方法都追加到Child，从而实现了继承 
*/
function Parent4() {
    this.username = 'keily';
    this.hello = function () { console.log(this.username); }
}
function Child4(password) {
    this.password = password;
}
Child4.prototype = new Parent4();
Child4.prototype.world = function () {
    var plog = { username: this.username, psw: this.password };
    console.log(plog);
}
var child4 = new Child4('123');
child4.hello();
child4.world();

/*
5、继承的第五种方式：混合方式 
  混合了call方式、原型链方式 
*/
function Parent5(username) {
    this.username = username;
}
Parent5.prototype.hello = function () { console.log(this.username); }
function Child5(username, password) {
    Parent5.call(this, username); //将父类的属性继承过来 
    this.password = password; //新增一些属性 
}
Child5.prototype = new Parent5(); //将父类的方法继承过来 
Child5.prototype.world = function () {//新增一些方法 
    var plog = { username: this.username, psw: this.password };
    console.log(plog);
}
var parent5 = new Parent5('lisi');
var child5 = new Child5("zhangsan", "123");
parent5.hello();
child5.hello();
child5.world();
