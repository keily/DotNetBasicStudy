function ObjectToString2(OppObj) {
  if(OppObj.constructor==Array){
    return '['+OppObj.join(',')+']';
  }
  if(typeof(OppObj)!="object"){
    return OppObj;
  }
  var rst = "{";
  var copy = OppObj;
  var aPrototype = new Object();
  for (var member in OppObj) {
      rst += member + ":" + ObjectToString2(copy[member]) + ',';
  }
  return rst.substring(0, rst.length - 1)+'}';
}

var obj={a:1,b:2,c:{c1:45,c2:43},d:[34,67]};
var strObj = ObjectToString2(obj);
var obj2=eval('('+strObj +')');
