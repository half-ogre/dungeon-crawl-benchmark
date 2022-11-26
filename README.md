# Dungeon Crawl Benchmark

I love reading posts about .NET and Go performance and benchmarks, but I never really know how pertinent they are to how *I* build and deploy web apps. I have been mulling a few new side projects, one of which is buildung a narrative dungeon crawl game that rests somewhere between a full-blown role-playing game and a choose your own adventure story. I thought it might be fun to combine that side project with an apples-to-apples, side-by-side performance comparison of the same app built in both .NET and in Go.

So that's what's going on in this repo.

## The Game

The game will be a text-based dungeon crawl that uses a simple and light RPG ruleset similar to older versions of Dungeons and Dragons. You will have a character with equipment and different moves you can make (e.g., attack, cast a spell, sneak, etc.). There will be rooms, monsters, NPCs, and a story that unfolds as the dungeon is explored.

## The App

To keep things simple, this app will itself be simple and stick to basic web app stuff that can be done comparably in .NET and Go. The apps will use the same external auth (Auth0) and datastore. They will be deployed the same (containers in Kubernetes) to the same cloud infrastructure (in Azure). All app features and behavior will be available via API endpoints and there will be a server-side rendered front end. (Yes, an API isn't really needed given this isn't a platform and the front end is on the back end, but most apps I build need an API so I want that to be part of this.)

## The Approach

I will build the two apps first focused on "geting it done" and "normal" (i.e. not foused on performance) development. I will then pick different aspects of the apps and run performance tests on them both with and without load. I might then work on performance optimization for each and do the performance testing again, but then again I might not.

## The Results

We shall see.