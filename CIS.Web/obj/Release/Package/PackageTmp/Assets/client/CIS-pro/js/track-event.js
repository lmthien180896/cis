function trackEvent(code,url){

    d=new Date();
    t =d.getTime();
	var e=new Date();
    var x = e.getTime();
    var exit = Math.round((x - t)/1000);
	 _gaq.push(['_trackEvent', code, 'Document Downloaded',url,exit]);


}