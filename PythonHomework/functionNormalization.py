from inputData import x, u
import numpy as np
import bisect as bs

debugMode = False

for i in range(len(x)-1):
     if x[i+1] < x[i]:
          i_end = i
          break

for i in range(i_end, len(x)-1):
     if x[i+1] > x[i]:
          i_start = i
          break


print(f"ambiguous region: [{x[i_start]}, {x[i_end]}]")
print(f"ambiguous indices are between {i_end} and {i_start}")

# todo: find x0 between x[i_start], x[i_end]
# e.g.  brute-force search

# taking part of the array from i_end to i_start in inversed order
# so the result will be sorted in INCREASING way
# (i_end) = elem_1 < ... < elem_n < (i_start)

def pseudoCross(a, b):
    return a[0] * b[1] - a[1] * b[0]

if debugMode:
    print(f"check pseudo cross 1: {pseudoCross( [0, 1], [1, 0] )}")
    print(f"check pseudo cross 2: {pseudoCross( [1, 0], [0, 1] )}")

def orientedSquare(figure):
    sqAcc = 0
    for i in range(len(figure) - 1):
        sqAcc += pseudoCross(figure[i], figure[i + 1])
    sqAcc += pseudoCross(figure[-1], figure[0])
    return sqAcc / 2

if debugMode:
    print(f"check oriented square: {orientedSquare( [ [-1, 0], [0, 1], [1, 0] ] )}")

seqX = x[i_end:i_start + 1]
seqX.reverse()

seqU = u[i_end:i_start + 1]
seqU.reverse()

points = list(zip(seqX, seqU))

eps = 1e-6
diff = 1

# variables for classical binary search algorithm
left = seqX[0]
right = seqX[-1]
mx = 0

# binary search
while abs(diff) > eps:
    mx = left + (right - left) / 2

    # find mi that seqX[mi - 1] <= mx < seqX[mi]
    mi = bs.bisect_left(seqX, mx)

    if debugMode:
        print(f"midX = {mx}")
        print(f"midIndex = {mi}")

    # find u coordinate of middle-point on the section of curve
    tg = (seqU[mi] - seqU[mi - 1]) / (seqX[mi] - seqX[mi - 1])
    mu = seqU[mi - 1] + tg * (mx - seqX[mi - 1])

    if debugMode:
        print(f"midU = {mu}")

    mPoint = [mx, mu]

    leftFigure = points[0:mi]
    leftFigure.append(mPoint)

    rightFigure = points[mi:-1]
    rightFigure.append(mPoint)

    # left figure has positive oriented square
    leftSquare = orientedSquare(leftFigure)
    # right figure has negative oriented square
    rightSquare = (-1) * orientedSquare(rightFigure)

    if debugMode:
        print(f"left figure square = {leftSquare}")
        print(f"right figure square = {rightSquare}")

    diff = rightSquare - leftSquare

    if (diff > 0):
        right = mx
    else:
        left = mx
    
print(f"Solution is x0 = {mx}")