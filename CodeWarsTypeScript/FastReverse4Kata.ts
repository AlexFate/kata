const reverse = (a: any[]) : any[] => a.map((t,i)=>{[t,a[a.length-i-1]]=[a[a.length-i-1],t];return t;});

//const reverse2 = (t: any[]) : any[] => t.map((e,i,a)=>a[a.length-1-i]);
const reverse2 = (t: any[]) : any[] => t.push(t.shift());

const reverse3 = (t: any[]) : any[] => t.sort(_=>-1);

const reverse4 = ([h,...t]) => [...reverse(t),h];

const reverse5 = (t: any[]) : any[] => t.reduce((c,b)=>([b,...c]),[]);



console.log(reverse2([1,2,3,'a','b','c',[]]));
console.log(reverse2([0, undefined]));
console.log(reverse2([0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]));


