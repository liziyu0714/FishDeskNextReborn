using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishDeskNextReborn.Helpers
{
    public class argResolver
    {
        public delegate void ResolverDelegate ();
        public static Dictionary<string,ResolverDelegate> resolvers = new Dictionary<string, ResolverDelegate>();
        public static void Register(string arg, ResolverDelegate resolve)
        {
            resolvers.Add(arg, resolve);
        }
        public static void Unregister(string arg)
        {
            resolvers.Remove(arg);
        }

        public static void Resolve(string[] args)
        {
            foreach (string arg_registered in resolvers.Keys)
            {
                if(args.Contains(arg_registered))
                    resolvers[arg_registered].Invoke();
            }
        }

        
    }
}
