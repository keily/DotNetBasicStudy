function ObjectToString2(OppObj) {
  if(typeof(OppObj)!="object"){
    return OppObj;
  }
    var rst = "{";
    var copy = OppObj;
    var aPrototype = new Object()
    for (var member in OppObj) {
        rst += member + ":" + ObjectToString2(copy[member]) + ',';
    }
    return rst.substring(0, rst.length - 1)+'}';
}
