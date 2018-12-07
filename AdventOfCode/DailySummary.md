# Advent of Code daily summary

## Day 1
* ranks 7829 / 6019
* done by 06:26:57
* 46 LOC
* Summing a list of values
* Keeping a running total and history and looking for the first repetition of running total.
* Mostly easy (*)
* Didn't really work on using functional or other new concepts


## Day 2
* ranks 2398 / 2281
* done by 01:02:47
* 57 LOC
* Finding differences and repetitions between text
* Learned that I can pattern match two lists x :: xs at the same time.
* Fairly easy (*)


## Day 3
* ranks 16477 / 15360
* done by 19:13:11
* 183 LOC
* Finding overlapping squares in a collection of lattice rectanges
* Parsing particularly formatted data.

This was a real challenge, but I learned a lot.
* How to capture parsed values from capturing groups of a regex
* Doing binary search in F#

* I had to think on how to do the rectangle overlap problem, before the binary split idea came to me.
* The idea of splitting claims that are in between the two new regions was a new thing too.

* Difficult (***)

## Day 4
* ranks 5786 / 5448
* done by 07:42:16
* 149 LOC
* Finding times guards are asleep from event logs
* Parsing even more detailed formatted data.

This was a challenge, but not as intense as day 3, partly because of what day 3 helped me with.
* How to capture multi alternative types of log line.
* Doing a fancy functional thing: folding

* It wasn't nearly as gnarly as day 3, but it did take some working on, to nail down the behavior expected.
* Processing data / state sequentially using a fold is pretty cool

* Moderately Difficult (**)

## Day 5
* ranks 8019 / 7694
* done by 07:48:35
* 81 LOC
* Finding an efficient way to repetitive react strings
* Performance analysis and optimization
* I had three versions of the main algorithm
* V1 - recursive on each line, recursive on the reapplication level (VERY SLOW)
* V2 - Regexes for each iteration, recursive for reapplication (SLOW)
* V3 - Realized you can completely react - react - react locally, moving across the string once
* This was more than 10 times faster.
* Moderately Difficult (**)

## Day 6
* ranks 5208 / 4718
* done by 8:24:05
* 125 LOC
* Finding a region closer to a point than all other selected points
*
* I was trying to find some smart, super fast algorithm
* And struggled mightily on this, 
* until I went back to a brute force solution
* Which, to my surprise, ran very fast
*
* This was difficult because I was looking for a Voronoi approach or some mathematical or compsci reduction
* It didn't have to be
* Xtra Difficult (****)

## Day 7
* ranks 900 / 1212
* done by 1:48:28
* 178 LOC
* Finding an order for Jobs, when Jobs have dependencies on other jobs
*
* Part 1 seemed fairly straightforward
* Part 2 was hard only because I was tired and hacked something together
* Most LOC of the month so far
* Moderately Difficult (**)

# Charts

### Ranking Part 1 (* = 1000, 1-9 = 100 - 900)
1. Day 1 - *******
2. Day 2 - **
3. Day 3 - ****************
4. Day 4 - *****
5. Day 5 - ********
6. Day 6 - *****
7. Day 7 - 9

### Part 1 vs Part 2

1. Day 1 - 7829 to 6019 : -1810
2. Day 2 - 2398 to 2281 : -117
3. Day 3 - 16477 to 15360 : -1117
4. Day 4 - 5786 to 5448 : -338
5. Day 5 - 8019 to 7694 : -325
6. Day 6 - 5208 to 4718 : -490
7. Day 7 - 900 to 1212: +312

Day 7 broke my streak of improving from part 1 to part 2.
Its earlier in the day, so the statistical curve I was in was different,
but also, it took me an extra hour to do part 2.

