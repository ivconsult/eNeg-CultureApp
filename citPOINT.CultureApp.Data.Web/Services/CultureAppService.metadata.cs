
namespace citPOINT.CultureApp.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies ConversationCultureMetadata as the class
    // that carries additional metadata for the ConversationCulture class.
    [MetadataTypeAttribute(typeof(ConversationCulture.ConversationCultureMetadata))]
    public partial class ConversationCulture
    {

        // This class allows you to attach custom attributes to properties
        // of the ConversationCulture class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ConversationCultureMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ConversationCultureMetadata()
            {
            }

            public Guid ConversationCultureID { get; set; }

            public Nullable<Guid> ConversationID { get; set; }

            public CultureFiveDimension CultureFiveDimension { get; set; }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public Nullable<int> PartnerCultureID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CultureFiveDimensionMetadata as the class
    // that carries additional metadata for the CultureFiveDimension class.
    [MetadataTypeAttribute(typeof(CultureFiveDimension.CultureFiveDimensionMetadata))]
    public partial class CultureFiveDimension
    {

        // This class allows you to attach custom attributes to properties
        // of the CultureFiveDimension class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CultureFiveDimensionMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CultureFiveDimensionMetadata()
            {
            }

            public EntityCollection<ConversationCulture> ConversationCultures { get; set; }

            public int CultureID { get; set; }

            public EntityCollection<DomainCultureMapping> DomainCultureMappings { get; set; }

            public Nullable<int> IDV { get; set; }

            public Nullable<int> LTO { get; set; }

            public Nullable<int> MAS { get; set; }

            public EntityCollection<NegotiationCulture> NegotiationCultures { get; set; }

            public Nullable<int> PDI { get; set; }

            public Nullable<int> UAI { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies DomainCultureMappingMetadata as the class
    // that carries additional metadata for the DomainCultureMapping class.
    [MetadataTypeAttribute(typeof(DomainCultureMapping.DomainCultureMappingMetadata))]
    public partial class DomainCultureMapping
    {

        // This class allows you to attach custom attributes to properties
        // of the DomainCultureMapping class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class DomainCultureMappingMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private DomainCultureMappingMetadata()
            {
            }

            public CultureFiveDimension CultureFiveDimension { get; set; }

            public Nullable<int> CultureID { get; set; }

            public int DomainCultureMappingID { get; set; }

            public string DomainExt { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies NegotiationCultureMetadata as the class
    // that carries additional metadata for the NegotiationCulture class.
    [MetadataTypeAttribute(typeof(NegotiationCulture.NegotiationCultureMetadata))]
    public partial class NegotiationCulture
    {

        // This class allows you to attach custom attributes to properties
        // of the NegotiationCulture class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class NegotiationCultureMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private NegotiationCultureMetadata()
            {
            }

            public CultureFiveDimension CultureFiveDimension { get; set; }

            public Nullable<int> DefaultCultureID { get; set; }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public Guid NegotiationCultureID { get; set; }

            public byte NegotiationCultureType { get; set; }

            public Nullable<Guid> NegotiationID { get; set; }
        }
    }
}
