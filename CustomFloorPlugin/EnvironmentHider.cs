﻿using System;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CustomFloorPlugin
{
    /// <summary> 
    /// Activates and deactivates world geometry in the active scene as required by CustomPlatforms
    /// </summary>
    public class EnvironmentHider
    {
        private ArrayList feet;
        private ArrayList originalPlatform;
        private ArrayList smallRings;
        private ArrayList bigRings;
        private ArrayList visualizer;
        private ArrayList towers;
        private ArrayList highway;
        private ArrayList backColumns;
        private ArrayList doubleColorLasers;
        private ArrayList backLasers;
        private ArrayList rotatingLasers;
        private ArrayList trackLights;

        public static bool showFeetOverride = false;

        /// <summary>
        /// Hide and unhide world objects as required by a platform
        /// </summary>
        /// <param name="platform">A platform that defines which objects are to be hidden</param>
        public void HideObjectsForPlatform(CustomPlatform platform)
        {
            if (feet != null) SetCollectionHidden(feet, (platform.hideDefaultPlatform && !showFeetOverride));
            if (originalPlatform != null) SetCollectionHidden(originalPlatform, platform.hideDefaultPlatform);
            if (smallRings != null) SetCollectionHidden(smallRings, platform.hideSmallRings);
            if (bigRings != null) SetCollectionHidden(bigRings, platform.hideBigRings);
            if (visualizer != null) SetCollectionHidden(visualizer, platform.hideEQVisualizer);
            if (towers != null) SetCollectionHidden(towers, platform.hideTowers);
            if (highway != null) SetCollectionHidden(highway, platform.hideHighway);
            if (backColumns != null) SetCollectionHidden(backColumns, platform.hideBackColumns);
            if (backLasers != null) SetCollectionHidden(backLasers, platform.hideBackLasers);
            if (doubleColorLasers != null) SetCollectionHidden(doubleColorLasers, platform.hideDoubleColorLasers);
            if (rotatingLasers != null) SetCollectionHidden(rotatingLasers, platform.hideRotatingLasers);
            if (trackLights != null) SetCollectionHidden(trackLights, platform.hideTrackLights);
        }

        /// <summary>
        /// Finds all GameObjects that make up the default environment
        /// and groups them into array lists
        /// </summary>
        public void FindEnvironment()
        {
            FindFeetIcon();
            FindOriginalPlatform();
            FindSmallRings();
            FindBigRings();
            FindVisualizers();
            FindTowers();
            FindHighway();
            FindBackColumns();
            FindBackLasers();
            FindRotatingLasers();
            FindDoubleColorLasers();
            FindTrackLights();
        }

        /// <summary>
        /// Set the active state of a Collection of GameObjects
        /// </summary>
        /// <param name="arlist">An ArrayList of GameObjects</param>
        /// <param name="hidden">A boolean describing the desired hidden state</param>
        private void SetCollectionHidden(ArrayList arlist, bool hidden)
        {
            if (arlist == null) return;
            foreach (GameObject go in arlist)
            {
                if (go != null) go.SetActive(!hidden);
            }
        }

        /// <summary>
        /// Finds a GameObject by name and adds it to the provided ArrayList
        /// </summary>
        /// <param name="name">The name of the desired GameObject</param>
        /// <param name="alist">The ArrayList to be added to</param>
        private bool FindAddGameObject(string name, ArrayList alist)
        {
            GameObject go = GameObject.Find(name);
            if (go != null)
            {
                alist.Add(go);
                return true;
            }
            return false;
        }

        private void FindFeetIcon()
        {
            feet = new ArrayList();
            GameObject feetgo = GameObject.Find("Feet");
            if (feetgo != null)
            {
                feet.Add(feetgo);
                feetgo.transform.parent = null; // remove from original platform 
            }
        }

        private void FindOriginalPlatform()
        {
            originalPlatform = new ArrayList();
            FindAddGameObject("Environment/PlayersPlace", originalPlatform);
            FindAddGameObject("Wrapper/MenuPlayersPlace", originalPlatform);
            FindAddGameObject("Wrapper/NeonLight (13)", originalPlatform);
        }

        private void FindSmallRings()
        {
            smallRings = new ArrayList();
            FindAddGameObject("Environment/SmallTrackLaneRings", smallRings);
            foreach (TrackLaneRing trackLaneRing in Resources.FindObjectsOfTypeAll<TrackLaneRing>().Where(x => x.name == "TrackLaneRing(Clone)"))
            {
                smallRings.Add(trackLaneRing.gameObject);
            }
            FindAddGameObject("Environment/TriangleTrackLaneRings", smallRings); // Triangle Rings from TriangleEnvironment
            foreach (TrackLaneRing trackLaneRing in Resources.FindObjectsOfTypeAll<TrackLaneRing>().Where(x => x.name == "TriangleTrackLaneRing(Clone)"))
            {
                smallRings.Add(trackLaneRing.gameObject);
            }
            // KDA
            FindAddGameObject("Environment/TentacleLeft", smallRings);
            FindAddGameObject("Environment/TentacleRight", smallRings);
        }
        
        private void FindBigRings()
        {
            bigRings = new ArrayList();
            FindAddGameObject("Environment/BigTrackLaneRings", bigRings);
            foreach (var trackLaneRing in Resources.FindObjectsOfTypeAll<TrackLaneRing>().Where(x => x.name == "BigTrackLaneRing(Clone)"))
            {
                bigRings.Add(trackLaneRing.gameObject);
            }
        }

        private void FindVisualizers()
        {
            visualizer = new ArrayList();
            FindAddGameObject("Environment/Spectrograms", visualizer);
        }
        
        private void FindTowers()
        {
            towers = new ArrayList();
            // Song Environments
            FindAddGameObject("Environment/Buildings", towers);
            FindAddGameObject("Environment/NearBuildingRight (1)", towers);
            FindAddGameObject("Environment/NearBuildingLeft (1)", towers);
            FindAddGameObject("Environment/NearBuildingLeft", towers);
            FindAddGameObject("Environment/NearBuildingRight", towers);

            // Menu
            FindAddGameObject("Wrapper/NearBuildingRight (1)", towers);
            FindAddGameObject("Wrapper/NearBuildingLeft (1)", towers);
            FindAddGameObject("Wrapper/NearBuildingLeft", towers);
            FindAddGameObject("Wrapper/NearBuildingRight", towers);

            // Monstercat
            FindAddGameObject("Environment/MonstercatLogoL", towers);
            FindAddGameObject("Environment/MonstercatLogoR", towers);

            // KDA
            FindAddGameObject("Environment/FloorL", towers);
            FindAddGameObject("Environment/FloorR", towers);
            if (FindAddGameObject($"Environment/GlowLine", towers))
            {
                for (int i = 0; i < 100; i++)
                {
                    FindAddGameObject($"Environment/GlowLine ({i})", towers);
                }
            }

            FindAddGameObject("Wrapper/NeonLight (19)", towers);
            FindAddGameObject("Wrapper/NeonLight (20)", towers);
        }
        
        private void FindHighway()
        {
            highway = new ArrayList();
            FindAddGameObject("Environment/Floor", highway);
            FindAddGameObject("Environment/FloorConstruction", highway);
            FindAddGameObject("Environment/Construction", highway);
            FindAddGameObject("Environment/TrackConstruction", highway);
            FindAddGameObject("Environment/TrackMirror", highway);

            FindAddGameObject($"Environment/Cube", highway);
            for (int i = 1; i <= 10; i++)
            {
                FindAddGameObject($"Environment/Cube ({i})", highway);
            }

            //Menu
            FindAddGameObject("Wrapper/LeftSmallBuilding", highway);
            FindAddGameObject("Wrapper/RightSmallBuilding", highway);
            FindAddGameObject("Wrapper/NeonLight (17)", highway);
            FindAddGameObject("Wrapper/NeonLight (18)", highway);
        }
        
        private void FindBackColumns()
        {
            backColumns = new ArrayList();
            FindAddGameObject("Environment/BackColumns", backColumns);
            FindAddGameObject("Environment/BackColumns (1)", backColumns);

            FindAddGameObject("Wrapper/BackColumns", backColumns);
            FindAddGameObject("Wrapper/CeilingLamp", backColumns);
        }

        private void FindRotatingLasers()
        {
            rotatingLasers = new ArrayList();
            // Default, BigMirror, Triangle
            FindAddGameObject("Environment/RotatingLasersPair (6)", rotatingLasers);
            FindAddGameObject("Environment/RotatingLasersPair (5)", rotatingLasers);
            FindAddGameObject("Environment/RotatingLasersPair (4)", rotatingLasers);
            FindAddGameObject("Environment/RotatingLasersPair (3)", rotatingLasers);
            FindAddGameObject("Environment/RotatingLasersPair (2)", rotatingLasers);
            FindAddGameObject("Environment/RotatingLasersPair (1)", rotatingLasers);
            FindAddGameObject("Environment/RotatingLasersPair", rotatingLasers);

            // Nice Env
            FindAddGameObject("Environment/RotatingLasersLeft0", rotatingLasers);
            FindAddGameObject("Environment/RotatingLasersLeft1", rotatingLasers);
            FindAddGameObject("Environment/RotatingLasersLeft2", rotatingLasers);
            FindAddGameObject("Environment/RotatingLasersLeft3", rotatingLasers);
            FindAddGameObject("Environment/RotatingLasersRight0", rotatingLasers);
            FindAddGameObject("Environment/RotatingLasersRight1", rotatingLasers);
            FindAddGameObject("Environment/RotatingLasersRight2", rotatingLasers);
            FindAddGameObject("Environment/RotatingLasersRight3", rotatingLasers);
        }

        private void FindDoubleColorLasers()
        {
            doubleColorLasers = new ArrayList();

            // Default, BigMirror, Nice, 
            FindAddGameObject("Environment/DoubleColorLaser", doubleColorLasers);
            FindAddGameObject("Environment/DoubleColorLaser (1)", doubleColorLasers);
            FindAddGameObject("Environment/DoubleColorLaser (2)", doubleColorLasers);
            FindAddGameObject("Environment/DoubleColorLaser (3)", doubleColorLasers);
            FindAddGameObject("Environment/DoubleColorLaser (4)", doubleColorLasers);
            FindAddGameObject("Environment/DoubleColorLaser (5)", doubleColorLasers);
            FindAddGameObject("Environment/DoubleColorLaser (6)", doubleColorLasers);
            FindAddGameObject("Environment/DoubleColorLaser (7)", doubleColorLasers);
            FindAddGameObject("Environment/DoubleColorLaser (8)", doubleColorLasers);
            FindAddGameObject("Environment/DoubleColorLaser (9)", doubleColorLasers);
        }

        private void FindBackLasers()
        {
            backLasers = new ArrayList();
            FindAddGameObject("Environment/FrontLights", backLasers);
            
        }

        private void FindTrackLights()
        {
            trackLights = new ArrayList();
            FindAddGameObject("Environment/GlowLineR", trackLights);
            FindAddGameObject("Environment/GlowLineL", trackLights);
            FindAddGameObject("Environment/GlowLineR2", trackLights);
            FindAddGameObject("Environment/GlowLineL2", trackLights);
            FindAddGameObject("Environment/GlowLineFarL", trackLights);
            FindAddGameObject("Environment/GlowLineFarR", trackLights);
            
            //KDA
            FindAddGameObject("Environment/GlowLineLVisible", trackLights);
            FindAddGameObject("Environment/GlowLineRVisible", trackLights);
            
            //KDA, Monstercat
            FindAddGameObject("Environment/Laser", trackLights);
            for (int i = 0; i < 15; i++)
            {
                FindAddGameObject($"Environment/Laser ({i})", trackLights);
            }
            FindAddGameObject("Environment/GlowTopLine", trackLights);
            for (int i = 0; i < 10; i++)
            {
                FindAddGameObject($"Environment/GlowTopLine ({i})", trackLights);
            }

            // Monstercat
            FindAddGameObject("Environment/GlowLineLHidden", trackLights);
            FindAddGameObject("Environment/GlowLineRHidden", trackLights);
        }
    }
}
