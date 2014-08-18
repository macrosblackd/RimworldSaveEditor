using System;
using System.Linq;
using System.Threading;
using RimWorld;
using UnityEngine;
using Verse;

namespace RimWorldDefDumperMod
{
    public class Loader : Def
    {
        static Loader()
        {
            Log.Error("<CINIT>(..cctor) Section invocation ((static)Type Constructor)");
            var watcher = new Thread(WatchInternalState) {IsBackground = true};
            watcher.Start();
            //we start new thread here to monitor game activity and field change, this done in place of bytecode injection.
        }

        static void WatchInternalState()
        {
            while(true) //infinite loop
            {
                try //just in case, normal exceptions wont show up in console
                {
                    if (!Application.isPlaying) break;
                    Thread.Sleep(100);

                    if (BackstoryDatabase.allBackstories == null || !BackstoryDatabase.allBackstories.Any()) continue;

                    //if (Application.loadedLevelName.EqualsIgnoreCase("gameplay")) break;
                    
                    
                    Log.Message("Trying to dump back stories");
                    new BackstoryDumper().Dump();

                    //if (Find.RootMap == null) continue; //as long as map not initialized we do nothing

                    //Thread.Sleep(100); //when map is initialized, we wait to ensure object init and perform payload
                    //if (!BackstoryDatabase.allBackstories.Any()) continue;
                    

                    

                    //throw GameInitializeEvent and handle all mods that listen to it NYI
                    Log.Message("Dumped back stories");
                    break; //we no longer need this thread, we break loop and terminate it
                }
                catch (Exception ex)
                {
                    Log.Error(ex.ToString());
                }
            }
            Log.Message("Done watching");
        }
    }
}
