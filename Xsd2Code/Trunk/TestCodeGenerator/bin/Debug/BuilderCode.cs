def authoringConceptReference = new AuthoringConceptReferenceBuilder()
	.withId("91")
	.withDisplayName("Dose")
	.withIsZynxTerm("false");

def externalConceptReference = new ExternalConceptReferenceBuilder()
	.withEnvironmentId("91")
	.withIdspace("client")
	.withId("Enum1")
	.withSecondaryKey("Enum1")
	.withUri("http://localhost/api/v1/catalog/env/91/enumerations/client,Enum1");

def authoringConceptReference = new AuthoringConceptReferenceBuilder()
	.withId("91")
	.withDisplayName("Aspirin")
	.withIsZynxTerm("false");

def externalConceptReference = new ExternalConceptReferenceBuilder()
	.withEnvironmentId("619")
	.withIdspace("client")
	.withId("AspirinTermId")
	.withSecondaryKey("AspirinTermIdSecondaryKey")
	.withUri("http://localhost/api/v1/catalog/env/91/sections/client,AspirinTermId");

def referenceBinding = new ReferenceBindingBuilder()
	.withExternalConceptReference(externalConceptReference)
	.withAuthoringConceptReference(authoringConceptReference);

def referenceBinding = new ReferenceBindingBuilder()
	.withExternalConceptReference(externalConceptReference)
	.withAuthoringConceptReference(authoringConceptReference);

def enumerationValueReferences = new EnumerationValueReferencesBuilder()
	.withReferenceBinding(referenceBinding);

def dataElementReference = new DataElementReferenceBuilder()
	.withEnvironmentId("91")
	.withIdspace("client")
	.withId("DT2")
	.withSecondaryKey("DT2")
	.withUri("http://localhost/api/v1/catalog/env/91/dataelements/client,DT2");

def dataElementReference = new DataElementReferenceBuilder()
	.withEnvironmentId("91")
	.withIdspace("client")
	.withId("DT1")
	.withSecondaryKey("DT1")
	.withUri("http://localhost/api/v1/catalog/env/91/dataelements/client,DT1");

def sectionReferences = new SectionReferencesBuilder()
	.withReferenceBinding(referenceBinding);

def dataElementInstance = new DataElementInstanceBuilder()
	.withDataElementReference(dataElementReference)
	.withEnumerationValueReferences(enumerationValueReferences);

def dataElementInstance = new DataElementInstanceBuilder()
	.withDataElementReference(dataElementReference)
	.withFreeValue("100");

def folder = new FolderBuilder()
	.withId("26603")
	.withName("ShowCaseFolder");

def orderSetSection = new OrderSetSectionBuilder()
	.withSectionReferences(sectionReferences);

def dataElementInstances = new DataElementInstancesBuilder()
	.withDataElementInstance(dataElementInstance)
	.withDataElementInstance(dataElementInstance);

def dataTemplateReference = new DataTemplateReferenceBuilder()
	.withEnvironmentId("91")
	.withIdspace("client")
	.withId("OrderSetADT")
	.withSecondaryKey("OrderSetADT")
	.withUri("http://localhost/api/v1/catalog/env/91/datatemplates/client,OrderSetADT");

def synchronizationValidation = new SynchronizationValidationBuilder()
	.withErrors("false")
	.withWarnings("false");

def folder = new FolderBuilder()
	.withId("26592")
	.withName("Our Content")
	.withFolder(folder);

def topLevelItems = new TopLevelItemsBuilder()
	.withOrderSetSection(orderSetSection);

def appliedData = new AppliedDataBuilder()
	.withDataTemplateReference(dataTemplateReference)
	.withDataElementInstances(dataElementInstances);

def metadata = new MetadataBuilder()
	.withTitle("OrderSet Released")
	.withStatus("Released")
	.withComments("This is a comments.")
	.withFolder(folder)
	.withSynchronizationValidation(synchronizationValidation)
	.withLastModifiedDataTime("2013-08-06 14:09:00")
	.withExpirationDate("2015-08-01")
	.withStaged("false")
	.withIsLinkable("true")
	.withIsPublishedToViewspace("true");

def orderSetIdentifier = new OrderSetIdentifierBuilder()
	.withEnvironmentId("619")
	.withId("42166")
	.withVersion("1")
	.withUri("
			http://localhost/api/v1/orderSets/env/619/full/42166/ver/1
		")
	.withSummaryUri("
			http://localhost/api/v1/orderSets/env/619/summary/42166/ver/1
		");

def orderSet = new OrderSetBuilder()
	.withOrderSetIdentifier(orderSetIdentifier)
	.withMetadata(metadata)
	.withAppliedData(appliedData)
	.withTopLevelItems(topLevelItems);

