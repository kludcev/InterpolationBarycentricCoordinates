Interpolation by using barycentric coordinates c#.

This project shows how to interpolate values defined at vertices across the tetrahedron in 3 – dimensional space. 
Barycentric coordinates in 3d space are four numbers (λ1, λ2, λ3, λ4) corresponding to masses placed at the vertices of a reference tetrahedron ABCD. 
These masses then determine a point P, which is the geometric centroid of the four masses and is identified with coordinates (λ1, λ2, λ3, λ4). 

Interpolation algorithm looks this way:
1)	Initialization of the point P, which is the geometric centroid we explore.
2)	Initialization of 4 points, which are the vertices of tetrahedron ABCD. 
3)	We check if the point P is inside the ABCD tetrahedron. If so, => steps 4 and 5. If not – usage of this method is not appropriate. 
4)	The calculation of the barycentric coordinates.
5)	Using barycentric coordinates obtained on the previous step, we calculate mass of the point P. 


Result: 
Barycentric coordinates
For point A: 0.28
For point B: 0.22
For point C: 0.31
For point D: 0.18
FindPoint Characteristics: 26.24
