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


# Charts

### Ranking Part 1 (* = 1000)
1. Day 1 - *******
2. Day 2 - **
3. Day 3 - ****************
4. Day 4 - *****

### Part 1 vs Part 2

1. Day 1 - 7829 to 6019 : -1810
2. Day 2 - 2398 to 2281 : -117
3. Day 3 - 16477 to 15360 : -1117
4. Day 4 - 5786 to 5448 : -338

So I'm getting a better ranking on the 2nd part of the day, universally, so far.

For day 3 and day 4, the second part was easy compared to the setup of part 1