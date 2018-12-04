namespace AdventOfCode

module Day3 =
    open Day1
    open Day2
    open System.IO
    open System.Text
    open System.Text.RegularExpressions
    open Utils
    open System.Collections.Generic
    open System.Linq
    
    let rawClaims = File.ReadAllLines("Day3input.txt")

    // Format:
    // (Claim Number) @ (xs),(ys): (xw)x(yw)
    // #1 @ 483,830: 24x18
    
    let claimRegex = new Regex(@"#(\d+) @ (\d+),(\d+): (\d+)x(\d+)")

    type Claim = 
        {
            ClaimNumber : int;
            XStart : int;
            YStart : int;
            XWidth : int;
            YWidth : int;
        }

    let getNumberFromCapture (claimMatch : Match) (index : int) =
        parseInt (claimMatch.Groups.[index].Value)

    let toClaim (rawClaim : string) = 
        let claimMatch = claimRegex.Match(rawClaim)

        {
            ClaimNumber = getNumberFromCapture claimMatch 1;
            XStart = getNumberFromCapture claimMatch 2;
            YStart = getNumberFromCapture claimMatch 3;
            XWidth = getNumberFromCapture claimMatch 4;
            YWidth = getNumberFromCapture claimMatch 5;
        }

    let claims = 
        rawClaims
        |> Array.map toClaim

    // We have a set of rectangular regions, and we want to see how many squares overlap between at least 2 rectangles
    // Array: xs - xw, ys - yw
    
    // We have an area of at least 1000 x 1000, and 1375 rectangles.
    // A naive solution could really churn through a lot of CPU power.
    // xs, xw, ys, yw -> system of inequalities: xs <= x < xs + xw, ys <= y < ys + yw

    // We can split the plane into halves, quarters, etc, and if a rectangle is intervening into both halves, we can replace it with a rectangle in each side.
    // By this way, we can check what is in each square in an efficient way.

    // recursive function:
    // getOverlaps claims region : claim

    let splitsX (leftRegion : Claim) (rightRegion : Claim) (claims : Claim list) = 
        let leftXS = leftRegion.XStart
        let leftXW = leftRegion.XWidth
        let rightXS = rightRegion.XStart

        let splitClaimX (claim : Claim) = 
            if claim.XStart < leftXS + leftXW 
            then
                if claim.XStart + claim.XWidth - 1 >= rightXS
                then
                    // We know claim starts in the left, and goes into the right
                    // rightXS - claimXS will give you the width of the left
                    // claimXW - (rightXS - claimXS) will give you the width of the right
                    (Some({ ClaimNumber = claim.ClaimNumber; XStart = claim.XStart; YStart = claim.YStart; XWidth = rightXS - claim.XStart; YWidth = claim.YWidth; }),
                     Some({ ClaimNumber = claim.ClaimNumber; XStart = rightXS; YStart = claim.YStart; XWidth = claim.XWidth - (rightXS - claim.XStart); YWidth = claim.YWidth; })
                    )
                else
                // Claim is only on the left, use original region
                    (Some(claim), None)
            else
            // Claim starts in the right region, so no left region required, use original region
                (None, Some(claim))

        let claimsLR = 
            claims
            |> List.map splitClaimX
        
        let lclaims = 
            claimsLR
            |> List.map fst
            |> List.choose id

        let rclaims = 
            claimsLR
            |> List.map snd
            |> List.choose id

        (lclaims, rclaims)

    let splitsY (upRegion : Claim) (downRegion : Claim) (claims : Claim list) = 
        let upYS = upRegion.YStart
        let upYW = upRegion.YWidth
        let downYS = downRegion.YStart

        let splitClaimY (claim : Claim) = 
            if claim.YStart < upYS + upYW 
            then
                if claim.YStart + claim.YWidth - 1 >= downYS
                then
                    (Some({ ClaimNumber = claim.ClaimNumber; XStart = claim.XStart; YStart = claim.YStart; XWidth = claim.XWidth; YWidth = downYS - claim.YStart; }),
                     Some({ ClaimNumber = claim.ClaimNumber; XStart = claim.XStart; YStart = downYS; XWidth = claim.XWidth; YWidth = claim.YWidth - (downYS - claim.YStart); })
                    )
                else
                // Claim is only on the up, use original region
                    (Some(claim), None)
            else
            // Claim starts in the down region, so no up region required, use original region
                (None, Some(claim))

        let claimsUD = 
            claims
            |> List.map splitClaimY
        
        let uclaims = 
            claimsUD
            |> List.map fst
            |> List.choose id

        let dclaims = 
            claimsUD
            |> List.map snd
            |> List.choose id

        (uclaims, dclaims)
        
    let claimDict = new Dictionary<int, int>()

    let updateClaimDict (nclaims : int) (claims : Claim list) = 
        let addlScore = nclaims - 1 
        
        let updateCD (claim : Claim) = 
            if claimDict.ContainsKey(claim.ClaimNumber)
            then claimDict.[claim.ClaimNumber] <- claimDict.[claim.ClaimNumber] + addlScore
            else claimDict.Add(claim.ClaimNumber, addlScore)

        claims
        |> List.iter updateCD

    let rec getOverlaps (region : Claim) (claims : Claim list) = 
        match region with
        | { ClaimNumber = _; XStart = xs; YStart = ys; XWidth = 1; YWidth = 1 } -> 
            let nclaims = List.length claims
            updateClaimDict nclaims claims

            if nclaims > 1 then 1 else 0
        | { ClaimNumber = _; XStart = xs; YStart = ys; XWidth = xw; YWidth = 1 } -> 
            // xs, xw : xs <= x <= xs + xw
            // 3, 4 : 3, 4, 5, 6 => (3, 4) (5,6) => 3, 2 & 5, 2
            // left width = xw / 2, right width = xw - xw / 2
            // left start = xs, right start = xs + left width
            let leftRegion = { ClaimNumber = 0; XStart = xs; XWidth = xw / 2; YStart = ys; YWidth = 1; }
            let rightRegion = { ClaimNumber = 0; XStart = xs + xw / 2; XWidth = xw - xw / 2; YStart = ys; YWidth = 1; }
            
            let (leftSplits, rightSplits) = splitsX leftRegion rightRegion claims
            (getOverlaps leftRegion leftSplits + getOverlaps rightRegion rightSplits)

        | { ClaimNumber = _; XStart = xs; YStart = ys; XWidth = xw; YWidth = yw } ->
            let upRegion = { ClaimNumber = 0; XStart = xs; XWidth = xw; YStart = ys; YWidth = yw / 2; }
            let downRegion = { ClaimNumber = 0; XStart = xs; XWidth = xw; YStart = ys + yw / 2; YWidth = yw - yw / 2; }

            let (upSplits, downSplits) = splitsY upRegion downRegion claims
            (getOverlaps upRegion upSplits + getOverlaps downRegion downSplits)

    let startRegion = { ClaimNumber = 0; XStart = 0; XWidth = 1024; YStart = 0; YWidth = 1024; }

    let totalOverlaps = getOverlaps startRegion (claims |> List.ofArray)

    let claimKVP = Enumerable.AsEnumerable(claimDict)

    let nonoverlapIDs = 
        claimKVP
        |> Seq.filter (fun kvp -> kvp.Value = 0)
        |> Seq.map (fun kvp -> kvp.Key)

    