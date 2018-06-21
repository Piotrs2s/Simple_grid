Structural grid is discretization of the surface. In other words its continious object/shape 
presented in the form of smaller calculation areas (elements). There are many types, variations 
and modifications of structural grids.

What it does: 
Program allows to choose size of single element and generates a simple 
grid of triangle elements. It also records position of elements nodes which allows to obtain 
connection matrix (ordered list of elements nodes and it coordinates).

How it works:
Firs step is distribution of points with a given distance (size), then points are 
properly connected into Element objects (represented by 3 points), and saved to 
list of elements.



Application:
Structural grid is basic tool of the finite element method (FEM) that have very wide application
(e.g. modeling, modeling of phenomena, visualisation)
